using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AutoIECalcCmd
{
    class Log
    {
        public static void INFO(string text)
        {
            Record("INFO", text);
        }

        public static void ERRO(string text)
        {
            Record("ERRO", text);
        }

        public static void WARN(string text)
        {
            Record("WARN", text);
        }

        private static void Record(string level, string text)
        {
            string str = string.Format("{0} [{1}] {2}", DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), level, text);
            Console.WriteLine(str);

            FileStream fs = new FileStream("AutoIECalc.log", FileMode.Append, FileAccess.Write);
            StreamWriter _sw = new StreamWriter(fs);

            _sw.WriteLine(str);
            _sw.Flush();
            _sw.Close();

            fs.Close();
        }
    }
}
