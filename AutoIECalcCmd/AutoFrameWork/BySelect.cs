using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoFrameWork
{
    public static class By
    {
        public static Selector Name(string name)
        {
            return new SelectByName(name);
        }

        public static Selector NameContains(string name)
        {
            return new SelectByNameContains(name);
        }
    }

    public abstract class Selector
    {
        public abstract bool IsTrue(IntPtr hwnd);
        public abstract string desc();
    }

    public class SelectByName : Selector
    {
        public SelectByName(string name)
        {
            _name = name;
        }

        public override bool IsTrue(IntPtr hwnd)
        {
            string str = WinAPI.GetWindowText(hwnd);
            return str == _name;
        }

        public override string desc()
        {
            return "name equal:" + _name;
        }

        private string _name;
    }

    public class SelectByNameContains : Selector
    {
        public SelectByNameContains(string name)
        {
            _name = name;
        }

        public override bool IsTrue(IntPtr hwnd)
        {
            string str = WinAPI.GetWindowText(hwnd);
            return str.Contains(_name);
        }

        public override string desc()
        {
            return "name contains:" + _name;
        }

        private string _name;
    }
}
