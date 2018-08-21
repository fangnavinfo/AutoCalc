using AutoIECalcCmd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AutoFrameWork
{
    public class UIItem
    {
        public UIItem(IntPtr hWnd, IntPtr hWndWin, string ctrlName) : this(hWnd, hWndWin, ctrlName, null)
        {

        }

        public UIItem(IntPtr hWnd, IntPtr hWndWin, string ctrlName, object param)
        {
            _hWnd = hWnd;
            _hWndWin = hWndWin;
            _Text = ctrlName;
            _param = param;
        }

        public void WaitExit(int second=5*60)
        {
            Log.INFO(string.Format("Waiting {0} [{1},{2}] Exit", this.GetType().Name, _Text, _hWnd.ToString("X8")));

            for (int i = 0; i < second; i++)
            {
                if (!WinAPI.IsWindowEnabled(_hWnd))
                {
                    Log.INFO(string.Format("{0} [{1},{2}] Exit", this.GetType().Name, _Text, _hWnd.ToString("X8")));
                    return;
                }

                Thread.Sleep(1000);
            }

            throw new Exception(string.Format("Wait {0} [{1},{2}] Exit Timeout!", this.GetType().Name, _Text, _hWnd.ToString("X8")));
        }

        protected IntPtr _hWnd;
        protected IntPtr _hWndWin;
        protected string _Text;
        protected object _param;
    }
}
