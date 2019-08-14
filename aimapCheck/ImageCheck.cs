using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenCvSharp;

namespace aimapCheck
{
    class ImageCheck
    {
        public ImageCheck(string path, int step, string rlstPath, string currDate)
        {
            projPath = path;
            strCCDPath = projPath + @"\RawData\CCD\";
            this.step = step;

            var temp = projPath.Substring(projPath.IndexOf("@@"));
            prjDate = temp.Split('-')[2];
            prjDate = String.Format("{0}:{1}:{2}", "20"+prjDate.Substring(0, 2), prjDate.Substring(2, 2), prjDate.Substring(4, 2));

            pathCheckRslt = rlstPath + @"\Exposure\" + currDate + @"\";
            Directory.CreateDirectory(pathCheckRslt + @"\Over");
            Directory.CreateDirectory(pathCheckRslt + @"\Under");

            reportFile = new StreamWriter(pathCheckRslt + "report.csv", true, Encoding.Unicode);
            totalFile = new StreamWriter(pathCheckRslt + "total.csv", true, Encoding.Unicode);

            var syncPath = Directory.GetFiles(projPath + @"\RawData\SYNC\", "*.sync").First();
            syncStr = File.ReadAllText(syncPath);
        }

        internal int Process()
        {
            //Log.INFO("process start");

            int rslt = 1;

            var imagesL = Directory.EnumerateFiles(strCCDPath + @"l\", "*.JPG", SearchOption.AllDirectories);
            var imagesR = Directory.EnumerateFiles(strCCDPath + @"r\", "*.JPG", SearchOption.AllDirectories);

            var images = new List<string>();
            images.AddRange(imagesL);
            //images.AddRange(imagesR);

            ThreadPool.SetMinThreads(20,20);
            using (var finished = new CountdownEvent(1))
            {
                for (int i = 0; i < images.Count; i+= step)
                {
                    finished.AddCount();
                    ThreadPool.QueueUserWorkItem((obj) =>
                    {
                        int index = (int)obj;
                            int over = 0;
                            int under = 0;
                            int total = 0;

                            Console.WriteLine(string.Format("{0}", index));

                            var image = images[index];

                        bool isRight = image.Contains(@"\r\");

                        Mat src = new Mat(image, ImreadModes.Grayscale);

                        int[] channels = { 0 };
                        int[] histSize = { 256 };
                        float[][] ranges = { new float[]{ 0, 256 } };
                        Mat hist = new Mat();
                        Cv2.CalcHist(new Mat[] { src }, channels, null, hist, 1, histSize, ranges);

                        List<int> rsltlist = new List<int>();
                        for (int j = 0; j < 256; j++)
                        {
                            rsltlist.Add((int)hist.At<float>(j));
                        }

                        total = rsltlist.Sum();
                        under = rsltlist.Take(50).Sum();
                        over = rsltlist.Skip(200).Sum();

                        hist = null;
                        src = null;

                        GC.Collect();
                        //ImageCheck.CheckExposure(image, ref total, ref over, ref under);

                        string flag = "";
                        if ((double)over / total > 0.8)
                        {
                            rslt = 0;
                            flag = "Over";
                        }
                        else if ((double)under / total > 0.8)
                        {
                            rslt = 0;
                            flag = "Under";
                        }

                        if(flag != "")
                        {
                            var loc = GetImageLocation(image.Substring(image.LastIndexOf('\\') + 1));
                            var dst = pathCheckRslt + @"\" + flag + @"\" + image.Substring(image.LastIndexOf("\\"));

                            File.Copy(image, dst);
                            var temp = image.Split('_').Last();
                            var time = string.Format("{0}:{1}:{2}", temp.Substring(0, 2), temp.Substring(2, 2), temp.Substring(4, 2));
                            exif.Exif.PRV_Operate(dst, Convert(double.Parse(loc.lat)), Convert(double.Parse(loc.lon)), 0, prjDate + " " + time);

                            ReportExposure(image.Replace(strCCDPath, ""), total, over, under, flag, loc);
                        }

                        RecordTotalInfo(image.Replace(strCCDPath, ""), total, over, under, flag);

                        Console.WriteLine(string.Format("{0}", image));
                        finished.Signal();
                    }, i);

                }

                finished.Signal();
                finished.Wait();

            }

            return rslt;
            //Log.INFO("process finish");

        }

        private void RecordTotalInfo(string image, int total, int over, int under, string flag)
        {
            lock (totalFile)
            {
                if (totalFile.BaseStream.Position == 0)
                {

                    totalFile.WriteLine("总像素数, 过曝像素数, 欠曝像素数, 过曝比例, 欠曝比例, 图片路径", Encoding.Unicode);
                }



                string sentence = string.Format("{0}, {1}, {2}, {3}, {4}, {5}",
                                                total, over, under, over * 100 / total, under * 100 / total, image);

                totalFile.WriteLine(sentence);
                totalFile.Flush();
            }
        }

        private void ReportExposure(string image, int total, int over, int under, string type, (string lat, string lon) loc)
        {
            lock(reportFile)
            {
                if (reportFile.BaseStream.Position == 0)
                {

                    reportFile.WriteLine("欠/过曝类型, 总像素数, 过曝像素数, 欠曝像素数, 过曝比例, 欠曝比例, 经度, 纬度, 图片路径");
                }

                

                string sentence = string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}",
                                                type, total, over, under, over * 100 / total, under * 100 / total, loc.lat, loc.lon, image);

                reportFile.WriteLine(sentence);
                reportFile.Flush();

                
            }

        }

        private double Convert(double lon)
        {
            int i = (int)(lon / 100);
            return i + (lon % 100) / 60;
        }

        private (string lat, string lon) GetImageLocation(string image)
        {
            string imageNO = "N"+image.Split('_')[1];
            int index = syncStr.IndexOf(imageNO);

            string pre = "";
            string next = "";

            int preIndex = index;
            while (true)
            {
                int start = syncStr.IndexOf("GPRMC", preIndex);
                int end = syncStr.IndexOf("\n", start);
                pre = syncStr.Substring(start, end - start);
                if (!pre.Contains(",,,"))
                {
                    break;
                }

                preIndex = end;
            }

            int nextIndex = index;
            while (true)
            {
                int start = syncStr.LastIndexOf("GPRMC", nextIndex);
                int end = syncStr.IndexOf("\n", start);

                next = syncStr.Substring(start, end - start);

                if (!next.Contains(",,,"))
                {
                    break;
                }

                nextIndex = start;
            }

            string time = image.Split('_')[2];
            time = time.Substring(0, time.LastIndexOf('.'));
            if (Math.Abs(double.Parse(time) - double.Parse(pre.Split(',')[1])*100) > Math.Abs(double.Parse(time) - double.Parse(next.Split(',')[1])*100))
            {
                return (next.Split(',')[3], next.Split(',')[5]);
            }
            else
            {
                return (pre.Split(',')[3], pre.Split(',')[5]);
            }

        }

        private string syncStr;
        private string projPath;
        private string strCCDPath;
        private int step;
        private string prjDate;

        public readonly string pathCheckRslt;
        private StreamWriter reportFile;
        private StreamWriter totalFile;
    }
}