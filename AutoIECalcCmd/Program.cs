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
        static void Main(string[] args)
        {
            Log.INFO("AUTO IE CALC START! args:" + string.Join(",", args));

            ICalcProcess calcProcess = null;
            string showinfo = "";

            using (Hook hook = new Hook(args))
            {
                try
                {
                    calcProcess = CalcFactory.Generate(args);
                    string output = calcProcess.Do();

                    showinfo = string.Format("AUTO IE CALC SUCCESS! output:{0}", output);
                    Log.INFO(showinfo);
                }
                catch (Exception e)
                {
                    showinfo = string.Format("AUTO IE CALC FAILED! {0}", e.ToString());
                    Log.ERRO(showinfo);

                    calcProcess.Dump();
                }
            }

            if (!(calcProcess is CalcProcBaseLoc))
            {
                MessageBox.Show(showinfo);
            }
        }
    }
}
