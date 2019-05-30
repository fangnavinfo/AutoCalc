using AutoIECalcCmd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoFrameWork
{
    class StaticText:UIItem
    {
        public StaticText(IntPtr hWnd, IntPtr hWndWin, string ctrlName) : base(hWnd, hWndWin, ctrlName)
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

        public static StaticText CreateByIndex(IntPtr hWnd, IntPtr hWndWin, int indexBaseZero, ref int currIndex)
        {
            int length = WinAPI.GetWindowTextLength(hWnd);
            StringBuilder className = new StringBuilder(length + 1000);
            WinAPI.GetClassName(hWnd, className, className.Capacity);

            uint processid = 0;
            WinAPI.GetWindowThreadProcessId(hWnd, out processid);

            //Console.WriteLine(string.Format("{0} {1}", hWnd.ToString("x8"), className));

            if (className.ToString() != "Static")
            {
                return null;
            }

            if (currIndex != indexBaseZero)
            {
                currIndex++;
                return null;
            }

            return (StaticText)Activator.CreateInstance(typeof(StaticText), hWnd, hWndWin, string.Format("Index[{0}]", indexBaseZero));
        }

        public string GetValue()
        {

            int length = WinAPI.GetWindowTextLength(_hWnd);

            StringBuilder TextName = new StringBuilder(length + 1000);
            WinAPI.GetWindowText(_hWnd, TextName, TextName.Capacity);

            string text = TextName.ToString();
            Log.INFO(string.Format("Get StaticText:[{0}, {1}] value:[{2}] Win:[{3}]", _hWnd.ToString("X8"), _Text, text, _hWndWin.ToString("X8")));

            return text;
        }

        private static bool CheckByName(IntPtr hWnd, String name)
        {

            int length = WinAPI.GetWindowTextLength(hWnd);
            StringBuilder windowName = new StringBuilder(length + 1);
            WinAPI.GetWindowText(hWnd, windowName, windowName.Capacity);

            StringBuilder className = new StringBuilder(length + 1000);
            WinAPI.GetClassName(hWnd, className, className.Capacity);

            //Console.WriteLine(string.Format("{0} {1} {2}", hWnd.ToString("x8"), className, windowName));

            if (className.ToString() != "Static")
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
