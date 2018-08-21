using AutoIECalcCmd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoFrameWork
{
    class ListView : UIItem
    {
        public ListView(IntPtr hWnd, IntPtr hWndWin, string ctrlName) : base(hWnd, hWndWin, ctrlName)
        {

        }

        public static ListView CreateByIndex(IntPtr hWnd, IntPtr hWndWin, int indexBaseZero, ref int currIndex)
        {
            int length = WinAPI.GetWindowTextLength(hWnd);
            StringBuilder className = new StringBuilder(length + 1000);
            WinAPI.GetClassName(hWnd, className, className.Capacity);

            uint processid = 0;
            WinAPI.GetWindowThreadProcessId(hWnd, out processid);

            //Console.WriteLine(string.Format("{0} {1}", hWnd.ToString("x8"), className));

            if (className.ToString() != "SysListView32")
            {
                return null;
            }

            if (currIndex != indexBaseZero)
            {
                currIndex++;
                return null;
            }
            return (ListView)Activator.CreateInstance(typeof(ListView), hWnd, hWndWin, string.Format("Index[{0}]", indexBaseZero));
        }

        internal string[] AllItem()
        {
            List<string> list = new List<string>();

            for (int i = 0; i < itemCount; i++)
            {
                string strText = WinAPI.GetListViewItemText(_hWnd, i);
                list.Add(strText);
            }
            return list.ToArray();
        }

        public int itemCount
        {
            get
            {
                int rowNum = WinAPI.GetListViewItemNum(_hWnd);
                return rowNum;
            }
        }
    }
}
