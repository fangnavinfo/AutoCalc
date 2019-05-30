using AutoIECalcCmd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AutoFrameWork
{
    public class UIItem
    {
        public UIItem(IntPtr hWnd, IntPtr hWndWin, string ctrlName) : this(hWnd, hWndWin, ctrlName, null)
        {

        }

        public UIItem(IntPtr hWnd, IntPtr hWndWin, string ctrlName, object param)
        {
            _hWnd = hWnd;
            _hWndWin = hWndWin;
            _Text = ctrlName;
            _param = param;
        }

        protected IntPtr _hWnd;
        protected IntPtr _hWndWin;
        protected string _Text;
        protected object _param;
    }
}
