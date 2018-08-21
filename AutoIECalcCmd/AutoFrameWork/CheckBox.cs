using AutoIECalcCmd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AutoFrameWork
{
    class CheckBox : UIItem
    {
        public CheckBox(IntPtr hWnd, IntPtr hWndWin, string ctrlName) : base(hWnd, hWndWin, ctrlName)
        {

        }

        public static CheckBox CreateByName(IntPtr hWnd, IntPtr hWndWin, String name)
        {
            if (CheckByName(hWnd, name))
            {
                return (CheckBox)Activator.CreateInstance(typeof(CheckBox), hWnd, hWndWin, name);
            }

            return null;
        }

        public void SetCheckFlag(bool isChecked)
        {
            while (!WinAPI.IsWindowEnabled(_hWnd))
            {
                Thread.Sleep(200);
            }

            Log.INFO(string.Format("Set Checkbox:[{0}], value{1}, hWnd[{2},{3}]", _Text, isChecked, _hWnd.ToString("X8"), _hWndWin.ToString("X8")));
            WinAPI.SetActiveWindow(_hWndWin);
            WinAPI.SetFocus(_hWnd);

            IntPtr lparam = IntPtr.Zero;
            if (isChecked)
            {
                lparam = (IntPtr)0; 
            }
            else
            {
                lparam = (IntPtr)1;
            }

            WinAPI.PostMessage(_hWnd, (int)WinAPI.ButtonMessages.BM_SETCHECK, lparam, (IntPtr)0);

            Thread.Sleep(1000);

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
