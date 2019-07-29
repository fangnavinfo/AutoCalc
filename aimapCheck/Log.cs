using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aimapCheck
{
    class Log
    {
        internal static void INFO(string logtxt)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "log.txt";
            var reportFile = new StreamWriter(path, true);
            reportFile.WriteLine(string.Format("[{0}] {1}", DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), logtxt));
            reportFile.Flush();
            reportFile.Close();

            Console.WriteLine(logtxt);
        }


    }
}
