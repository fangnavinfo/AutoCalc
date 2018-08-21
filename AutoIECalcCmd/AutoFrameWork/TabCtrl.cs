using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoIECalcCmd;
using System.Runtime.InteropServices;

namespace AutoFrameWork
{
    class TabCtrl:UIItem
    {
        public TabCtrl(IntPtr hWnd, IntPtr hWndWin, string ctrlName) : base(hWnd, hWndWin, ctrlName)
        {

        }

        public static TabCtrl CreateByIndex(IntPtr hWnd, IntPtr hWndWin, int indexBaseZero, ref int currIndex)
        {
            int length = WinAPI.GetWindowTextLength(hWnd);
            StringBuilder className = new StringBuilder(length + 1000);
            WinAPI.GetClassName(hWnd, className, className.Capacity);

            uint processid = 0;
            WinAPI.GetWindowThreadProcessId(hWnd, out processid);

            //Console.WriteLine(string.Format("{0} {1}", hWnd.ToString("x8"), className));

            if (className.ToString() != "SysTabControl32")
            {
                return null;
            }

            if (currIndex != indexBaseZero)
            {
                currIndex++;
                return null;
            }

            return (TabCtrl)Activator.CreateInstance(typeof(TabCtrl), hWnd, hWndWin, string.Format("TabCtrl[{0}]", indexBaseZero));
        }

        public void Select(string value)
        {
            Log.INFO(string.Format("Select TabCtrl:[{0}] value:[{1}]", _Text, value));

            //Console.WriteLine(string.Format("{0} {1}", hWnd.ToString("x8"), winClass));

            int index = -1;
            int rowNum = WinAPI.GetTabCtrlItemNum(_hWnd);
            for (int i = 0; i < rowNum; i++)
            {
                string strText = WinAPI.GetTabCtrlItemText(_hWnd, i);
                if (strText == value)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                throw new Exception(string.Format("Can not find value[{0}] in TabCtrl:[{1}]", value, _Text));
            }

            WinAPI.SendMessage(_hWnd, (int)WinAPI.TabCtrlMessage.TCM_SETCURFOCUS, (IntPtr)(index), IntPtr.Zero);
            WinAPI.SendMessage(_hWnd, (int)WinAPI.TabCtrlMessage.TCM_SETCURSEL, (IntPtr)(index), IntPtr.Zero);
        }
    }
}
