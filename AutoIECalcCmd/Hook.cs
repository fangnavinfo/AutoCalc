using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoIECalcCmd
{
    class Hook : IDisposable
    {
        public Hook(string[] args)
        {
            if (args.Length > 0 && args.Where(x => x == "HIDE").FirstOrDefault() != null)
            {
                isHook = true;
            }

            if (isHook)
            {
                SetHook();
            }
        }

        public void Dispose()
        {
            if (isHook)
            {
                EndHook();
            }
        }

        [DllImport("HideHook.dll")]
        private static extern bool SetHook();

        [DllImport("HideHook.dll")]
        private static extern bool EndHook();



        private bool isHook = false;
    }
}
