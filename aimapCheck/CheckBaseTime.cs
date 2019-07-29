using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace aimapCheck
{
    class CheckBaseTime
    {
        private string prjpath;
        private string rlstpath;

        public CheckBaseTime(string prjpath, string rlstpath, string currDate)
        {
            this.prjpath = prjpath;
            this.rlstpath = rlstpath + "/CheckBaseRoverTime/" + currDate + "/";
        }

        public void Process()
        {
            //Report.LOG("=========================Base station and rover station time check=========================");

            //            工程名称：(以@@开头)
            //基站文件名称：aaa、bbb（文件名称）
            //流动站采集时间段：2019.03.22 9:00:00 - 2019.03.11 17:00:00
            //aaa基站采集时间段：2019.03.22 8:00:00 - 2019.03.22 19:00:00
            //bbb基站采集时间段：2019.03.22 8:00:00 - 2019.03.22 19:00:00
            //aaa基站采集时间是否连续：是 / 否
            //bbb基站采集时间是否连续：是 / 否
            //aaa中断时间段：
            //2019.03.22 9:00:00 - 2019.03.22 10:03:00
            //2019.03.22 11:00:00 - 2019.03.22 12:00:00
            //bbb中断时间段：
            //2019.03.22 9:00:00 - 2019.03.22 10:03:00
            //2019.03.22 11:00:00 - 2019.03.22 12:00:00

            var listNeedCheckSatellite = new List<(string name, DateTime start, DateTime end)>();

            var syncFile = Directory.EnumerateFiles(prjpath + @"\RawData\SYNC", "*.sync").First();
            var timeSync = GetSyncTimePeriod(syncFile);

            var baseFiles = Directory.EnumerateFiles(prjpath + @"\RawData\Base", "*.1?o");
            var timeBases = baseFiles.Select(x => (name: x.Substring(x.LastIndexOf(@"\")+1), value: GetBaseTime(x)));

            var discreteTimes = timeBases.Select(x => (name: x.name, value: GetDiscreteTimes(x.value)));

            string rlst = string.Format("工程文件名：{0}\r\n\r\n", prjpath.Substring(prjpath.IndexOf("@@")).TrimEnd('\\'));
            rlst += string.Format("基站文件名：{0}\r\n\r\n", string.Join(",", timeBases.Select(x=>x.name)));

            foreach (var time in timeBases)
            {
                var baseStart = DateTime.ParseExact(time.value.First(), "yyyy.MM.dd HH:mm:ss", CultureInfo.InvariantCulture);
                var baseEnd = DateTime.ParseExact(time.value.Last(), "yyyy.MM.dd HH:mm:ss", CultureInfo.InvariantCulture);

                var syncStart = DateTime.ParseExact(timeSync.start, "yyyy.MM.dd HH:mm:ss", CultureInfo.InvariantCulture);
                var syncEnd = DateTime.ParseExact(timeSync.end, "yyyy.MM.dd HH:mm:ss", CultureInfo.InvariantCulture);

                if (syncStart < baseStart || syncEnd > baseEnd)
                {
                    rlst += string.Format("{0}基站采集时间是否完全包含流动站时间：否\r\n", time.name);
                }
                else
                {
                    rlst += string.Format("{0}基站采集时间是否完全包含流动站时间：是\r\n", time.name);
                    listNeedCheckSatellite.Add((time.name, syncStart, syncEnd));
                }
            }
            rlst += "\r\n" + string.Format("流动站采集时间段：{0} - {1}", timeSync.start, timeSync.end);
            rlst += "\r\n" + string.Join("\r\n", timeBases.Select(x => string.Format("{0}基站采集时间段：{1} - {2}", x.name, x.value.First(), x.value.Last()))) + "\r\n";

            rlst += "\r\n" + string.Join("\r\n", discreteTimes.Select(x => string.Format("{0}基站采集时间是否连续：{1}", x.name, x.value.Count == 0 ? "是" : "否"))) + "\r\n";

            foreach(var timegroup in discreteTimes.Where(x=>x.value.Count != 0))
            {
                rlst += timegroup.name + "中断时间段:\r\n";
                rlst += string.Join("\r\n", timegroup.value.Select(x => string.Format("{0} - {1}", x.start, x.end))) + "\r\n";
            }

            Directory.CreateDirectory(rlstpath);
            string path = rlstpath + "/check_report.txt";

            File.WriteAllText(path, rlst, Encoding.UTF8);

            foreach(var elem in listNeedCheckSatellite)
            {
                var checker = new CheckBaseSatellite(prjpath + @"\RawData\Base\"+elem.name, elem.start, elem.end, 4);
                var checkRslt = checker.Run();
                if(checkRslt == null)
                {
                    checkRslt = string.Format("\r\n{0}基站是否有收星较差情况: 否\r\n", elem.name);
                }
                else
                {
                    checkRslt = string.Format("\r\n{0}基站是否有收星较差情况: 是\r\n", elem.name) + checkRslt;
                }
                File.AppendAllText(path, checkRslt, Encoding.UTF8);
            }
        }

        private List<string> GetBaseTime(string path)
        {
            List<string> times = new List<string>();
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);

            string timeline = "";
            while ((timeline = sr.ReadLine()) != null)
            {
                if (timeline.StartsWith("> "))
                {
                    timeline = timeline.Replace("> ", "");
                    var splits = timeline.Split(' ').Where(x => x.Length != 0).Select(x=> (int)double.Parse(x)).ToArray();
                    
                    var time = string.Format("{0:D4}.{1:D2}.{2:D2} {3:D2}:{4:D2}:{5:D2}", splits[0], splits[1], splits[2], splits[3], splits[4], splits[5]);
                    times.Add(time);
                }
            }

            times = times.Distinct().ToList();
            return times;
        }

        private List<(string start, string end)> GetDiscreteTimes(List<string> times)
        {
            List<(string start, string end)> rlst = new List<(string start, string end)>();

            for(int i=1; i< times.Count; i++)
            {

                DateTime prev = DateTime.ParseExact(times[i - 1], "yyyy.MM.dd HH:mm:ss",  CultureInfo.InvariantCulture);
                DateTime cur  = DateTime.ParseExact(times[i],     "yyyy.MM.dd HH:mm:ss", CultureInfo.InvariantCulture);

                var timeSpan = cur - prev;
                if(timeSpan.Seconds > 1)
                {
                    rlst.Add((times[i - 1], times[i]));
                }
            }

            return rlst;
        }

        private (string start, string end) GetBaseTimePeriod(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);

            string startline = "";
            while ((startline = sr.ReadLine()) != null)
            {
                if (startline.StartsWith("> "))
                {
                    startline = startline.Replace("> ", "");
                    break;
                }
            }

            var startsplits = startline.Split(' ');

            startline = startsplits[0] + startsplits[1] + startsplits[2] + "-" + startsplits[3] + startsplits[4] + startsplits[5];
            startline = startline.Substring(0, startline.IndexOf('.'));

            string temp = "";
            string endline = "";
            while ((temp = sr.ReadLine()) != null)
            {
                if (temp.StartsWith("> "))
                {
                    endline = temp.Replace("> ", "");
                }
            }

            var endsplits = endline.Split(' ');

            endline = endsplits[0] + endsplits[1] + endsplits[2] + "-" + endsplits[3] + endsplits[4] + endsplits[5];
            endline = endline.Substring(0, endline.IndexOf('.'));

            return (startline, endline);
        }

        private (string start, string end) GetSyncTimePeriod(string path)
        {
            StreamReader sr = new StreamReader(path);
            string startline = "";
            while ((startline = sr.ReadLine()) != null)
            {
                if (startline.StartsWith("$GPRMC"))
                {
                    break;
                }
            }

            string[] split = startline.Split(',');

            string day = split[9];
            string time = split[1].Split('.').First();
            day = string.Format("{0}.{1}.{2}", "20" + day.Substring(4, 2), day.Substring(2, 2), day.Substring(0, 2));
            time = string.Format("{0}:{1}:{2}", time.Substring(0, 2), time.Substring(2, 2), time.Substring(4, 2));
            var start = day + " " + time;

            string endline = "";
            string temp = "";
            while ((temp = sr.ReadLine()) != null)
            {
                if (temp.StartsWith("$GPRMC"))
                {
                    endline = temp;
                }
            }


            split = endline.Split(',');

            day = split[9];
            time = split[1];
            day = string.Format("{0}.{1}.{2}", "20" + day.Substring(4, 2), day.Substring(2, 2), day.Substring(0, 2));
            time = string.Format("{0}:{1}:{2}", time.Substring(0, 2), time.Substring(2, 2), time.Substring(4, 2));

            var end = day + " " + time;

            return (start, end);
        }
    }
}
