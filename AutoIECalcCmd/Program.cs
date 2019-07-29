using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoIECalcCmd
{
    class Program
    {
        static int Main(string[] args)
        {
            Log.INFO("AUTO IE CALC START! args:" + string.Join(",", args));

            ICalcProcess calcProcess = null;
            string showinfo = "";

            int ret = 0;
            using (Hook hook = new Hook(args))
            {
                try
                {
                    File.Delete(AutoIECalcPublic.ConfigSetting.errfile);

                    calcProcess = CalcFactory.Generate(args);
                    string output = calcProcess.Do();

                    showinfo = string.Format("AUTO IE CALC SUCCESS! output:{0}", output);
                    Log.INFO(showinfo);

                    ret = 2;
                }
                catch (Exception e)
                {
                    showinfo = string.Format("AUTO IE CALC FAILED! {0}", e.ToString());
                    Log.ERRO(showinfo);

                    File.WriteAllText(AutoIECalcPublic.ConfigSetting.errfile, showinfo);

                    Console.Error.WriteLine(showinfo);
                    calcProcess.Dump();

                    ret = 1;
                }
            }

            if (!(calcProcess is CalcProcBaseLoc) && args.Any(x=>x == "show_rslt"))
            {
                MessageBox.Show(showinfo);
            }

            return ret;
        }
    }
}
