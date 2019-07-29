using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aimapCheck
{
    class Report
    {
        public static void Init(string path)
        {
            Directory.CreateDirectory(path);
            reportFile = new StreamWriter(path + @"\report.log", true);
        }

        public static void LOG(string txt)
        {
            reportFile.WriteLine(txt);
            reportFile.Flush();
        }

        private static StreamWriter reportFile;
    }
}
