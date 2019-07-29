using System;
using System.IO;
using System.Linq;

namespace aimapCheck
{
    internal class DataIntegrityCheck
    {
        private string prjPath;

        public DataIntegrityCheck(string prjPath)
        {
            this.prjPath = prjPath;
        }

        internal void Process()
        {
            CheckCCD();
            CheckROVER();
            CheckSync();
        }

        private void CheckSync()
        {
            string syncPath = Directory.EnumerateFiles(prjPath + @"\RawData\SYNC", "*.sync").First();
            StreamReader sr = new StreamReader(syncPath);
            string line = "";
            int pretimer = -1;
            while ((line = sr.ReadLine()) != null)
            {
                if (!line.Contains("GPRMC"))
                {
                    continue;
                }

                int timer = ConvertSeconds(line.Split(',')[1]);
                if (pretimer != -1 && timer != pretimer + 1)
                {
                    throw new Exception(string.Format("{0} 两个相邻GPMRC时间间隔超过100 {1}, {2}", syncPath, pretimer, timer));
                }

                pretimer = timer;
            }

            sr.Close();
        }

        private void CheckROVER()
        {
            //throw new NotImplementedException();
        }

        private void CheckCCD()
        {
            string lpath = prjPath + @"\RawData\CCD\l";
            string rpath = prjPath + @"\RawData\CCD\r";

            if(!Directory.Exists(lpath))
            {
                throw new Exception("can not find path:" + lpath);
            }
            if (!Directory.Exists(rpath))
            {
                throw new Exception("can not find path:" + rpath);
            }

            int lostCount = 0;
            string[] ccdstrings = File.ReadAllLines(Directory.EnumerateFiles(prjPath + @"\RawData\CCD", "*.csv").First());
            foreach(var str in ccdstrings)
            {
                var words = str.Split(',');
                if (words.Length < 4 || words[2].Length == 0 || words[3].Length == 0)
                {
                    lostCount++;
                }
            }

            if(lostCount/ ccdstrings.Count() > 0.05)
            {
                throw new Exception(string.Format("左右目JPG照片不匹配的条数大于5%"));
            }

            //var imagesL = Directory.EnumerateFiles(lpath, "*.JPG", SearchOption.AllDirectories);
            //var imagesR = Directory.EnumerateFiles(rpath, "*.JPG", SearchOption.AllDirectories);
            //if(imagesR.Count() == imagesL.Count())
            //{
            //    throw new Exception(string.Format("左右目JPG照片个数不一致，左目：{0}，右目：{1}", imagesR.Count(), imagesL.Count()));
            //}

            
        }

        private int ConvertSeconds(string str)
        {
            if(str.Length < 6)
            {
                throw new Exception(string.Format("GPRMC 时间格式错误{0}", str));
            }

            int hour = int.Parse(str.Substring(0, 2));
            int min = int.Parse(str.Substring(2, 2));
            int second = int.Parse(str.Substring(4, 2));

            return second + min * 60 + hour * 60 * 60;
        }
    }
}