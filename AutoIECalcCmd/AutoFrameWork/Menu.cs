using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoIECalcCmd;

namespace AutoFrameWork
{
    public class Menu : UIItem
    {
        public Menu(uint uID, IntPtr hWnd, IntPtr hWndSub, IntPtr hWndWin, string ctrlName) : base(hWnd, hWndWin, ctrlName)
        {
            _uID = uID;
            this.hWndSub = hWndSub;
        }

        public void Click()
        {
            if (parent != null)
            {
                parent.Click();
            }
            
            if(_uID == UInt32.MaxValue)
            {
                return;
            }

            Log.INFO(string.Format("Click Menu:[{0}, {1}], hWnd[{2}]", _Text, _uID, _hWndWin.ToString("X8")));
            WinAPI.PostMessage(_hWndWin, (int)WinAPI.WMMessage.WM_COMMAND, (IntPtr)_uID, IntPtr.Zero);
        }

        public string name
        {
            get
            {
                return _Text;
            }
        }



        public IntPtr hWnd
        {
            get
            {
                return _hWnd;
            }
            set
            {
                 _hWnd = value;
            }
        }

        public IntPtr hWndWin
        {
            get
            {
                return _hWndWin;
            }
            set
            {
                if(parent != null)
                {
                    parent.hWndWin = value;
                }
                
                _hWndWin = value;
            }
        }

        public IntPtr hWndSub = IntPtr.Zero;

        public Menu parent = null;

        private uint _uID;
    }
}
