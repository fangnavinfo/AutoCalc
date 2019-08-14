using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aimapCheck
{
    class CheckBaseSatellite
    {
        private string prjpath;
        private string rlstpath;

        private string baseFile;
        private DateTime start;
        private DateTime end;
        private int minSatelliteNum;

        public CheckBaseSatellite(string baseFile, DateTime start, DateTime end, int minSatelliteNum)
        {
            this.baseFile = baseFile;
            this.start = start;
            this.end = end;
            this.minSatelliteNum = minSatelliteNum;
        }

        public string Run()
        {
            var list = new List<(DateTime pre, DateTime cur, int num)>();

            FileStream fs = new FileStream(baseFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);

            string line = null;

            DateTime? preTime = null;
            int GPSCount = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.StartsWith(">"))
                {
                    DateTime currTime = AnaylizeTime(line);

                    if (currTime > end)
                    {
                        break;
                    }
                    if (currTime < start)
                    {
                        continue;
                    }

                    if (preTime != null)
                    {
                        if (GPSCount < minSatelliteNum)
                        {
                            list.Add((preTime.Value, currTime, GPSCount));
                        }
                    }

                    preTime = currTime;
                    GPSCount = 0;
                    continue;
                }

                if (preTime == null)
                {
                    continue;
                }

                //GXX开头的是GPS卫星
                if (line[0] == 'G' && Char.IsDigit(line[1]) && Char.IsDigit(line[2]))
                {
                    GPSCount++;
                }
            }

            int i = 0;
            while (i < list.Count() - 1)
            {
                if (list[i].cur != list[i + 1].pre)
                {
                    i++;
                    continue;
                }

                list[i] = (list[i].pre, list[i + 1].cur, list[i + 1].num);
                list.RemoveAt(i + 1);
            }

            //for(int i=0; i<list.Count()-1; i++)
            //{
            //    if(list[i].cur == list[i+1].pre)
            //    {
            //        list[i] = (list[i].pre, list[i + 1].cur, list[i + 1].num);

            //        list.RemoveAt(i+1);
            //        i--;
            //    }
            //};
            if(list.Any())
            {
                var rslt = string.Format("收星较差时间段：\r\n{0}\r\n", string.Join("\r\n", list.Select(x => string.Format("{0}-{1}", x.pre.ToString("yyyy.MM.dd HH:mm:ss"), x.cur.ToString("yyyy.MM.dd HH:mm:ss")))));
                rslt += string.Format("收星较差时间段占比：{0}%\r\n", Math.Round(list.Select(x => x.cur.Subtract(x.pre).TotalSeconds).Sum() * 100.0 / end.Subtract(start).TotalSeconds, 2));
                return rslt;
            }

            return null;
        }

        private DateTime AnaylizeTime(string line)
        {
            line = line.Replace("> ", "");
            var splits = line.Split(' ').Where(x => x.Length != 0).Select(x => (int)double.Parse(x)).ToArray();

            var time = string.Format("{0:D4}.{1:D2}.{2:D2} {3:D2}:{4:D2}:{5:D2}", splits[0], splits[1], splits[2], splits[3], splits[4], splits[5]);
            return DateTime.ParseExact(time, "yyyy.MM.dd HH:mm:ss", CultureInfo.InvariantCulture);
        }

        //public CheckBaseSatellite(string v, string rlstpath, string currDate)
        //{
        //    this.prjpath = prjpath;
        //    this.rlstpath = rlstpath + "/CheckBaseRoverTime/" + currDate + "/";

        //}

    //    internal void Process()
    //    {
    //        Report.LOG("========================Check base station satellite num=========================");

    //        var syncFile = Directory.EnumerateFiles(prjpath + @"\RawData\SYNC", "*.sync").First();
    //        var timeSync = GetSyncTimePeriod(syncFile);

    //        var baseFiles = Directory.EnumerateFiles(prjpath + @"\RawData\Base", "*.1?o");
    //        var timeBases = baseFiles.Select(x => (name: x.Substring(x.LastIndexOf(@"\") + 1), value: GetBaseTimeWithSatelliteNum(x)));

    //        var startime = DateTime.ParseExact(timeSync.start, "yyyy.MM.dd HH:mm:ss", CultureInfo.InvariantCulture);
    //        var endtime = DateTime.ParseExact(timeSync.start, "yyyy.MM.dd HH:mm:ss", CultureInfo.InvariantCulture);

            
    //        foreach (var singeBase in timeBases)
    //        {
    //            bool? flag = null;

    //            var rlst = new List<(DateTime start, DateTime end, bool flag)>();
    //            DateTime predate = DateTime.Now;
    //            foreach (var elem in singeBase.value)
    //            {
    //                var basetime = DateTime.ParseExact(elem.time, "yyyy.MM.dd HH:mm:ss", CultureInfo.InvariantCulture);
    //                if (basetime < startime)
    //                {
    //                    continue;
    //                }

    //                if( basetime > endtime)
    //                {
    //                    break;
    //                }

                 
    //                if(elem.num > 4)
    //                {
    //                    if(flag == null)
    //                    {
    //                        flag = true;
    //                        predate = basetime;
    //                    }
    //                    if(flag == false)
    //                    {
    //                        flag = true;
                            
    //                        rlst.Add((predate, basetime, false));
    //                        predate = basetime;
    //                    }
    //                }
    //                else
    //                {
    //                    if (flag == null)
    //                    {
    //                        flag = false;
    //                        predate = basetime;
    //                    }
    //                    if(flag == true)
    //                    {
    //                        flag = false;

    //                        rlst.Add((predate, basetime, true));
    //                        predate = basetime;
    //                    }
    //                }
    //            }
               
    //        }
    //    }

    //    private List<(string time, int num)> GetBaseTimeWithSatelliteNum(string path)
    //    {
    //        List<(string, int num)> rslt = new List<(string, int num)>();
    //        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
    //        StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);

    //        string timeline = "";
    //        while ((timeline = sr.ReadLine()) != null)
    //        {
    //            if (timeline.StartsWith("> "))
    //            {
    //                timeline = timeline.Replace("> ", "");
    //                var splits = timeline.Split(' ').Where(x => x.Length != 0).Select(x => (int)double.Parse(x)).ToArray();

    //                var time = string.Format("{0:D4}.{1:D2}.{2:D2} {3:D2}:{4:D2}:{5:D2}", splits[0], splits[1], splits[2], splits[3], splits[4], splits[5]);
    //                var num = splits[7];
    //                rslt.Add((time, num));
    //            }
    //        }
    //        return rslt;
    //    }

    //    private (string start, string end) GetSyncTimePeriod(string path)
    //    {
    //        StreamReader sr = new StreamReader(path);
    //        string startline = "";
    //        while ((startline = sr.ReadLine()) != null)
    //        {
    //            if (startline.StartsWith("$GPRMC"))
    //            {
    //                break;
    //            }
    //        }

    //        string[] split = startline.Split(',');

    //        string day = split[9];
    //        string time = split[1].Split('.').First();
    //        day = string.Format("{0}.{1}.{2}", "20" + day.Substring(4, 2), day.Substring(2, 2), day.Substring(0, 2));
    //        time = string.Format("{0}:{1}:{2}", time.Substring(0, 2), time.Substring(2, 2), time.Substring(4, 2));
    //        var start = day + " " + time;

    //        string endline = "";
    //        string temp = "";
    //        while ((temp = sr.ReadLine()) != null)
    //        {
    //            if (temp.StartsWith("$GPRMC"))
    //            {
    //                endline = temp;
    //            }
    //        }


    //        split = endline.Split(',');

    //        day = split[9];
    //        time = split[1];
    //        day = string.Format("{0}.{1}.{2}", "20" + day.Substring(4, 2), day.Substring(2, 2), day.Substring(0, 2));
    //        time = string.Format("{0}:{1}:{2}", time.Substring(0, 2), time.Substring(2, 2), time.Substring(4, 2));

    //        var end = day + " " + time;

    //        return (start, end);
    //    }
    }
}
