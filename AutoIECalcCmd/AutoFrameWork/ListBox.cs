using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using AutoIECalcCmd;
using System.Threading;

namespace AutoFrameWork
{
    class ListBox : UIItem
    {
        public ListBox(IntPtr hWnd, IntPtr hWndWin, string ctrlName, int rowId) : base(hWnd, hWndWin, ctrlName, rowId)
        {

        }

        public static ListBox CreateByName(IntPtr hWnd, IntPtr hWndWin, String name)
        {
            int rowId = -1;
            if (CheckByName(hWnd, name, ref rowId))
            {
                return (ListBox)Activator.CreateInstance(typeof(ListBox), hWnd, hWndWin, name, rowId);
            }

            return null;
        }

        public void Click()
        {
            Log.INFO(string.Format("Click ListBox:[{0}, {1}] win[{2}] ", _Text, _hWnd.ToString("X8"), _hWndWin.ToString("X8")));

            IntPtr text = Marshal.StringToCoTaskMemUni(_Text);
            WinAPI.SendMessage(_hWnd, (int)WinAPI.ListBoxMessages.LB_SELECTSTRING, (IntPtr)(-1), _Text);
            Marshal.FreeCoTaskMem(text);

            Thread.Sleep(2000);

            IntPtr parenthWnd = WinAPI.GetWindowLongPtr(_hWnd, (int)WinAPI.GWL.GWL_HWNDPARENT);
            IntPtr nID = WinAPI.GetWindowLongPtr(_hWnd, (int)WinAPI.GWL.GWL_ID);
            int ctrl_id = nID.ToInt32();

            WinAPI.PostMessage(parenthWnd, (int)WinAPI.WMMessage.WM_COMMAND, (IntPtr)WinAPI.MakeWParam(ctrl_id, (int)WinAPI.ListBoxMessages.LBN_SELCHANGE), _hWnd);


            //WinAPI.LVITEM lvi = new WinAPI.LVITEM();
            //lvi.stateMask = (uint)WinAPI.LVITEM.STATE.LVIS_SELECTED;
            //lvi.state = (uint)WinAPI.LVITEM.STATE.LVIS_SELECTED;

            //IntPtr ptrLvi = Marshal.AllocHGlobal(Marshal.SizeOf(lvi));
            //Marshal.StructureToPtr(lvi, ptrLvi, false);

            //int rowId = (int)_param;
            //WinAPI.SendMessage(_hWnd, (int)WinAPI.ListBoxMessages.LB_SETCURSEL, new IntPtr(rowId), ptrLvi);
        }

        private static bool CheckByName(IntPtr hWnd, String name, ref int rowId)
        {
            string winClass = WinAPI.GetWinClass(hWnd);
            if(winClass != "ListBox")
            {
                return false;
            }

            //Console.WriteLine(string.Format("{0} {1}", hWnd.ToString("x8"), winClass));

            int rowNum = WinAPI.GetListBoxItemNum(hWnd);
            for (int i = 0; i < rowNum; i++)
            {
                string strText = WinAPI.GetListBoxItemText(hWnd, i);
                if(strText == name)
                {
                    rowId = i;
                    return true;
                }
            }

            return false;
        }
    }
}
