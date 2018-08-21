using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoIECalcCmd;
using System.Runtime.InteropServices;
using System.Threading;

namespace AutoFrameWork
{
    class Editor : UIItem
    {
        public Editor(IntPtr hWnd, IntPtr hWndWin, string ctrlName) : base(hWnd, hWndWin, ctrlName)
        {

        }

        public static Editor CreateByIndex(IntPtr hWnd, IntPtr hWndWin, int indexBaseZero, ref int currIndex)
        {
            int length = WinAPI.GetWindowTextLength(hWnd);
            StringBuilder className = new StringBuilder(length + 1000);
            WinAPI.GetClassName(hWnd, className, className.Capacity);

            uint processid = 0;
            WinAPI.GetWindowThreadProcessId(hWnd, out processid);

            //Console.WriteLine(string.Format("{0} {1}", hWnd.ToString("x8"), className));

            if (className.ToString() != "Edit")
            {
                return null;
            }

            if (currIndex != indexBaseZero)
            {
                currIndex++;
                return null;
            }

            return (Editor) Activator.CreateInstance(typeof(Editor), hWnd, hWndWin, string.Format("Index[{0}]", indexBaseZero));
        }

        public void SetValue(string value)
        {
            if(value == "")
            {
                Log.INFO(string.Format("Set Editor:[{0}, {1}] with default value:[{2}] Win:[{3}]", _Text, _hWnd.ToString("X8"), GetValue(), _hWndWin.ToString("X8")));
                return;
            }

            Log.INFO(string.Format("Set Editor:[{0}, {1}] value:[{2}] Win:[{3}]", _Text, _hWnd.ToString("X8"), value, _hWndWin.ToString("X8")));

            IntPtr text = Marshal.StringToCoTaskMemUni(value);
            WinAPI.SendMessage(_hWnd, (int)WinAPI.WMMessage.WM_SETTEXT, (IntPtr)(value.Length+1), value);
            Thread.Sleep(2000);
            Marshal.FreeCoTaskMem(text);

        }

        public string GetValue()
        {

            Int32 buffer_size = WinAPI.SendMessage(_hWnd, (int)WinAPI.WMMessage.WM_GETTEXTLENGTH, (IntPtr)0, (IntPtr)0).ToInt32();

            StringBuilder buffer = new StringBuilder(buffer_size+1);
            WinAPI.SendMessage(_hWnd, (int)WinAPI.WMMessage.WM_GETTEXT, (IntPtr)buffer_size+1, buffer);

            string text = buffer.ToString();
            Log.INFO(string.Format("Get Editor:[{0}, {1}] value:[{2}] Win:[{3}]", _hWnd.ToString("X8"), _Text, text, _hWndWin.ToString("X8")));

            return text;
        }
    }
}
