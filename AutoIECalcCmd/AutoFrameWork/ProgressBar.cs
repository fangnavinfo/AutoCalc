using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AutoFrameWork
{
    class ProgressBar : UIItem
    {
        public ProgressBar(IntPtr hWnd, IntPtr hWndWin, string ctrlName) : base(hWnd, hWndWin, ctrlName)
        {

        }

        public static ProgressBar CreateByIndex(IntPtr hWnd, IntPtr hWndWin, int indexBaseZero, ref int currIndex)
        {
            int length = WinAPI.GetWindowTextLength(hWnd);
            StringBuilder className = new StringBuilder(length + 1000);
            WinAPI.GetClassName(hWnd, className, className.Capacity);

            uint processid = 0;
            WinAPI.GetWindowThreadProcessId(hWnd, out processid);

            //Console.WriteLine(string.Format("{0} {1}", hWnd.ToString("x8"), className));

            if (className.ToString() != "msctls_progress32")
            {
                return null;
            }

            if (currIndex != indexBaseZero)
            {
                currIndex++;
                return null;
            }

            return (ProgressBar)Activator.CreateInstance(typeof(ProgressBar), hWnd, hWndWin, string.Format("Index[{0}]", indexBaseZero));
        }

        public void WaitFinish()
        {
            while(true)
            {
                int value = (int)WinAPI.SendMessage(_hWnd, (int)WinAPI.ProgressBarMessage.PBM_GETPOS, IntPtr.Zero, IntPtr.Zero);
                Console.WriteLine("Wait ProgressBar Finish" + value);

                Thread.Sleep(1000);
            }
            
        }
    }
}
