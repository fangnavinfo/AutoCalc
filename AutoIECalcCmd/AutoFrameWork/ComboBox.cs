using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoIECalcCmd;
using System.Runtime.InteropServices;
using System.Threading;

namespace AutoFrameWork
{
    class ComboBox : UIItem
    {
        public ComboBox(IntPtr hWnd, IntPtr hWndWin, string ctrlName) : base(hWnd, hWndWin, ctrlName)
        {

        }

        public static ComboBox CreateByIndex(IntPtr hWnd, IntPtr hWndWin, int indexBaseZero, ref int currIndex)
        {
            int length = WinAPI.GetWindowTextLength(hWnd);
            StringBuilder className = new StringBuilder(length + 1000);
            WinAPI.GetClassName(hWnd, className, className.Capacity);

            uint processid = 0;
            WinAPI.GetWindowThreadProcessId(hWnd, out processid);

            //Console.WriteLine(string.Format("{0} {1}", hWnd.ToString("x8"), className));

            if (className.ToString() != "ComboBox")
            {
                return null;
            }

            if (currIndex != indexBaseZero)
            {
                currIndex++;
                return null;
            }

            return (ComboBox)Activator.CreateInstance(typeof(ComboBox), hWnd, hWndWin, string.Format("Index[{0}]", indexBaseZero));
        }

        public void Select(string value)
        {
            Log.INFO(string.Format("Select ComboBox:[{0}] value:[{1}] Win:[{2}]", _Text, value, _hWndWin.ToString("X8")));

            IntPtr text = Marshal.StringToCoTaskMemUni(value);
            WinAPI.SendMessage(_hWnd, (int)WinAPI.ComboBoxMessage.CB_SELECTSTRING, (IntPtr)(-1), value);
            Marshal.FreeCoTaskMem(text);

            Thread.Sleep(2000);

            IntPtr parenthWnd = WinAPI.GetWindowLongPtr(_hWnd, (int)WinAPI.GWL.GWL_HWNDPARENT);
            IntPtr nID = WinAPI.GetWindowLongPtr(_hWnd, (int)WinAPI.GWL.GWL_ID);
            int ctrl_id = nID.ToInt32();

            WinAPI.PostMessage(parenthWnd, (int)WinAPI.WMMessage.WM_COMMAND, (IntPtr)WinAPI.MakeWParam(ctrl_id, (int)WinAPI.ComboBoxMessage.CBN_SELCHANGE), _hWnd);
        }
    }
}
