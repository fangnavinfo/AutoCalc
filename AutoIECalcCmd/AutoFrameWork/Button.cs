using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoIECalcCmd;
using System.Threading;

namespace AutoFrameWork
{
    public class Button : UIItem
    {
        public Button(IntPtr hWnd, IntPtr hWndWin, string ctrlName) : base(hWnd, hWndWin, ctrlName)
        {

        }

        public IntPtr hwnd
        {
            get
            {
                return base._hWnd;
            }
        }

        public static Button CreateByName(IntPtr hWnd, IntPtr hWndWin, String name)
        {
            if (CheckByName(hWnd, name))
            {
                return (Button) Activator.CreateInstance(typeof(Button), hWnd, hWndWin, name);
            }

            return null;
        }

        public void Click(int time=1000)
        {
            while(!WinAPI.IsWindowEnabled(_hWnd))
            {
                Thread.Sleep(200);
            }

            Log.INFO(string.Format("Click Button:[{0}], hWnd[{1},{2}]", _Text, _hWnd.ToString("X8"), _hWndWin.ToString("X8")));
            WinAPI.SetActiveWindow(_hWndWin);
            WinAPI.SetFocus(_hWnd);

            //WinAPI.SendMessage(_hWnd, (int)WinAPI.ButtonMessages.BM_CLICK, (IntPtr)0, (IntPtr)0);

            WinAPI.PostMessage(_hWnd, (int)WinAPI.WMMessage.WM_LBUTTONDOWN, (IntPtr)1, (IntPtr)0);
            WinAPI.PostMessage(_hWnd, (int)WinAPI.WMMessage.WM_LBUTTONUP, (IntPtr)1, (IntPtr)0);


            Thread.Sleep(time);

            //WinAPI.PostMessage
        }
        
        private static bool CheckByName(IntPtr hWnd, String name)
        {

            int length = WinAPI.GetWindowTextLength(hWnd);
            StringBuilder windowName = new StringBuilder(length + 1);
            WinAPI.GetWindowText(hWnd, windowName, windowName.Capacity);

            StringBuilder className = new StringBuilder(length + 1000);
            WinAPI.GetClassName(hWnd, className, className.Capacity);

            //Console.WriteLine(string.Format("{0} {1} {2}", hWnd.ToString("x8"), className, windowName));

            if (className.ToString() != "Button")
            {
                return false;
            }

            uint processid = 0;
            WinAPI.GetWindowThreadProcessId(hWnd, out processid);
            if (windowName.ToString().Replace("&", "") == name)
            {
                return true;
            }

            return false;
        }
    }
}
