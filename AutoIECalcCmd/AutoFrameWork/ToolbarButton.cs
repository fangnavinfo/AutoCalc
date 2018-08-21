using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoIECalcCmd;

namespace AutoFrameWork
{
    class ToolbarButton : UIItem
    {
        public ToolbarButton(IntPtr hWnd, IntPtr hWndWin, string ctrlName, int commandId) : base(hWnd,  hWndWin, ctrlName, commandId)
        {

        }

        public static ToolbarButton CreateByIndex(IntPtr hWnd, IntPtr hWndWin, int indexBaseZero, ref int currIndex)
        {
            int length = WinAPI.GetWindowTextLength(hWnd);
            StringBuilder className = new StringBuilder(length + 1000);
            WinAPI.GetClassName(hWnd, className, className.Capacity);

            uint processid = 0;
            WinAPI.GetWindowThreadProcessId(hWnd, out processid);

            if (className.ToString() != "ToolbarWindow32")
            {
                return null;
            }

            WinAPI.TBBUTTON tb = new WinAPI.TBBUTTON();
            WinAPI.GetToolbarButton(hWnd, indexBaseZero, ref tb);

            return (ToolbarButton)Activator.CreateInstance(typeof(ToolbarButton), hWnd, hWndWin, string.Format("ToolbarButton[{0}]", indexBaseZero), tb.idCommand);
        }

        internal void Click()
        {
            Log.INFO(string.Format("Click Toolbar Button:[{0}]", _Text));

            int param = (int)_param;
            WinAPI.PostMessage(_hWnd, (int)WinAPI.WMMessage.WM_COMMAND, (IntPtr)param, (IntPtr)0);
        }
    }
}
