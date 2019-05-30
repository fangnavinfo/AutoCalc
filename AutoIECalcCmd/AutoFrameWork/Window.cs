using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Reflection;
using AutoIECalcCmd;

namespace AutoFrameWork
{
    public class Window
    {
        //public static Window Find(string winName)
        //{
        //    int iCount = 0;
        //    while (iCount < 50)
        //    {
        //        IntPtr hWnd = WinAPI.FindWindow(null, winName);
        //        if (hWnd != IntPtr.Zero)
        //        {
        //            Log.INFO(string.Format("Finded Window:[{0},{1}]", winName, hWnd.ToString("X8")));
        //            return new Window(hWnd, winName);
        //        }

        //        iCount++;

        //        Thread.Sleep(200);
        //    }

        //    throw new ArgumentException(string.Format("can not find window:[{0}]", winName));
        //}

        //internal void SendKeyBoard(char v)
        //{
        //    uint code = WinAPI.MapVirtualKey(v, 0);
        //    uint lctrlParam = (0x00000001 | ((uint)WinAPI.VirtualKeyCode.VK_MENU << 16));
        //    WinAPI.PostMessage(_hWnd, (int)WinAPI.WMMessage.WM_KEYDOWN, (IntPtr)WinAPI.VirtualKeyCode.VK_MENU, (IntPtr)0);
        //    Thread.Sleep(500);
        //    uint lcodlParam = (0x00000001 | ((uint)code << 16));
        //    WinAPI.PostMessage(_hWnd, (int)WinAPI.WMMessage.WM_KEYDOWN, (IntPtr)code, (IntPtr)0);
        //    Thread.Sleep(500);
        //    //lctrlParam |= 0xC0000000;
        //    WinAPI.PostMessage(_hWnd, (int)WinAPI.WMMessage.WM_KEYDOWN, (IntPtr)code, (IntPtr)1);
        //    Thread.Sleep(500);
        //    //lcodlParam |= 0xC0000000;
        //    WinAPI.PostMessage(_hWnd, (int)WinAPI.WMMessage.WM_KEYDOWN, (IntPtr)WinAPI.VirtualKeyCode.VK_MENU, (IntPtr)1);
        //}

        //public static Window FindWindowContains(string subName)
        //{

        //    int iCount = 0;
        //    while (iCount < 100)
        //    {
        //        Window findWindow = null;

        //        WinAPI.EnumWindows(new WinAPI.EnumWindowsProc((hWnd, lParam) =>
        //                            {
        //                                uint pid = 0;

        //                                int length = WinAPI.GetWindowTextLength(hWnd);
        //                                StringBuilder windowName = new StringBuilder(length + 1);
        //                                WinAPI.GetWindowText(hWnd, windowName, windowName.Capacity);

        //                                if (!windowName.ToString().Replace("&", "").Contains(subName))
        //                                {
        //                                    return true;
        //                                }

        //                                findWindow = (Window)Activator.CreateInstance(typeof(Window), hWnd, subName);

        //                                return false;
        //                            }),
        //                        (IntPtr)0);

        //        if (findWindow != null)
        //        {
        //            Log.INFO(string.Format("Finded Window:[{0},{1}]", subName, findWindow._hWnd.ToString("X8")));
        //            return findWindow;
        //        }
        //        iCount++;

        //        Thread.Sleep(200);
        //    }

        //    throw new ArgumentException(string.Format("can not find window:[{0}]", subName));
        //}

        //public Window FindChildWindow(string subName)
        //{

        //    int iCount = 0;
        //    while (iCount < 100)
        //    {
        //        Window findWindow = null;

        //        WinAPI.EnumWindows(new WinAPI.EnumWindowsProc((hWnd, lParam) =>
        //                            {
        //                                uint pid = 0;

        //                                    int length = WinAPI.GetWindowTextLength(hWnd);
        //                                    StringBuilder windowName = new StringBuilder(length + 1);
        //                                    WinAPI.GetWindowText(hWnd, windowName, windowName.Capacity);

        //                                    if (WinAPI.GetParent(hWnd) != _hWnd)
        //                                    {
        //                                        return true;
        //                                    }
        //                                    if (windowName.ToString().Replace("&", "") != subName)
        //                                    {
        //                                        return true;
        //                                    }


        //                                    findWindow = (Window)Activator.CreateInstance(typeof(Window), hWnd, subName);

        //                                    return false;
        //                                }),
        //                        (IntPtr)0);

        //        if (findWindow != null)
        //        {
        //            Log.INFO(string.Format("Finded chid Window:[{0},{1}], in window:[{2},{3}]", subName, findWindow._hWnd.ToString("X8"), _name, _hWnd.ToString("X8")));
        //            return findWindow;
        //        }

        //        iCount++;

        //        Thread.Sleep(200);
        //    }

        //    throw new ArgumentException(string.Format("can not find child window:[{0}], in window:[{1}]", subName, _name));
        //}

        //public Window FindChildWindowContains(string subName)
        //{
        //    int iCount = 0;
        //    while (iCount < 200)
        //    {

        //        Window findWindow = null;

        //        WinAPI.EnumChildWindows(_hWnd, new WinAPI.EnumWindowsProc((hWnd, lParam) =>
        //                                {
        //                                    uint pid = 0;

        //                                    int length = WinAPI.GetWindowTextLength(hWnd);
        //                                    StringBuilder windowName = new StringBuilder(length + 1);
        //                                    WinAPI.GetWindowText(hWnd, windowName, windowName.Capacity);

        //                                    if (!windowName.ToString().Replace("&", "").Contains(subName))
        //                                    {
        //                                        return true;
        //                                    }

        //                                    findWindow = (Window)Activator.CreateInstance(typeof(Window), hWnd, subName);

        //                                    return false;
        //                                }),
        //                                0);

        //        if (findWindow != null)
        //        {
        //            Log.INFO(string.Format("Finded child Window:[{0},{1}], in window:[{2},{3}]", subName, findWindow._hWnd.ToString("X8"), _name, _hWnd.ToString("X8")));
        //            return findWindow;
        //        }

        //        iCount++;

        //        Thread.Sleep(500);
        //    }

        //    throw new ArgumentException(string.Format("can not find child window:[{0}], in window:[{1},{2}]", subName, _name, _hWnd));
        //}

        internal void WaitExit(string ctrlName, Action<Button> continueAction)
        {
            Log.INFO(string.Format("Waiting Window[{0},{1}] Exit", _name, _hWnd));

            while (WinAPI.IsWindow(_hWnd))
            {
                string currFindName = ctrlName;
                Type findType = typeof(Button);
                object findObj = null;

                WinAPI.EnumChildWindows(_hWnd, new WinAPI.EnumWindowsProc((hWnd, lParam) =>
                                                {

                                                    return EnumWindowsProc<Button>(hWnd, lParam, currFindName, ref findObj);
                                                }),
                                        (int)_hWnd);

                if (findObj != null)
                {
                    continueAction((Button)findObj);

                }


                Thread.Sleep(200);
            }
        }

        internal void WaitChildWindowThen(params object[] args)
        {
            Dictionary<string, Action<Window>> dict = new Dictionary<string, Action<Window>>();
            for(int i=0; i< args.Length; i+=2)
            {
                string key = args[i] as string;
                Action<Window> action = args[i+1] as Action<Window>;
                dict.Add(key, action);
            }

            int iCount = 0;
            while (iCount < 1000)
            {
                Window findWindow = null;

                WinAPI.EnumChildWindows(_hWnd, new WinAPI.EnumWindowsProc((hWnd, lParam) =>
                                    {
                                        int length = WinAPI.GetWindowTextLength(hWnd);
                                        StringBuilder windowName = new StringBuilder(length + 1);
                                        WinAPI.GetWindowText(hWnd, windowName, windowName.Capacity);

                                        string fid = null;
                                        foreach(var elem in dict)
                                        {
                                            if (windowName.ToString().Replace("&", "").Contains(elem.Key))
                                            {
                                                fid = elem.Key;
                                                break;
                                            }
                                        }

                                        if(fid == null)
                                        {
                                            return true;
                                        }

                                        findWindow = (Window)Activator.CreateInstance(typeof(Window), hWnd, fid);
                                        return false;
                                    }),
                                   0);

                if (findWindow != null)
                {
                    Log.INFO(string.Format("Finded Window:[{0},{1}]", findWindow._name, findWindow._hWnd.ToString("X8")));

                    dict[findWindow._name](findWindow);
                    return;
                }

                iCount++;

                Thread.Sleep(200);
            }

            throw new ArgumentException(string.Format("can not find windows:[{0}]", String.Join(",", dict.Keys)));

        }

        public T Get<T>(string ctrlName) where T : UIItem
        {

            int iCount = 0;
            while (iCount < 100)
            {
                string currFindName = ctrlName;
                Type findType = typeof(T);
                object findObj = null;

                WinAPI.EnumChildWindows(_hWnd, new WinAPI.EnumWindowsProc((hWnd, lParam) =>
                                            {
                                                
                                                return EnumWindowsProc<T>(hWnd, lParam, currFindName, ref findObj);
                                            }),
                                        (int)_hWnd);

                if (findObj != null)
                {
                    return (T)findObj;
                    
                }

                iCount++;

                Thread.Sleep(200);
            }

            throw new ArgumentException(string.Format("can not find control:[{0}], in window:[{1}]", ctrlName, _name));
        }

        public T TryGet<T>(string ctrlName) where T : UIItem
        {

            int iCount = 0;
            while (iCount < 100)
            {
                string currFindName = ctrlName;
                Type findType = typeof(T);
                object findObj = null;

                WinAPI.EnumChildWindows(_hWnd, new WinAPI.EnumWindowsProc((hWnd, lParam) =>
                {

                    return EnumWindowsProc<T>(hWnd, lParam, currFindName, ref findObj);
                }),
                                        (int)_hWnd);

                if (findObj != null)
                {
                    return (T)findObj;

                }

                iCount++;

                Thread.Sleep(200);
            }

            return null;
        }

        public T1 TryGet<T1>(int indexBaseZero) where T1 : UIItem
        {
            int iCount = 0;
            while (iCount < 50)
            {
                Type findType = typeof(T1);
                object findObj = null;

                int currIndex = 0;
                WinAPI.EnumChildWindows(_hWnd, new WinAPI.EnumWindowsProc((hWnd, lParam) =>
                {
                    return EnumWindowsProc<T1>(hWnd, lParam, indexBaseZero, ref currIndex, out findObj);
                }),
                                        (int)_hWnd);

                if (findObj != null)
                {
                    return (T1)findObj;

                }

                iCount++;

                Thread.Sleep(200);
            }

            return null;
        }

        public List<T> GetS<T>(string ctrlName) where T : UIItem
        {

            int iCount = 0;
            while (iCount < 50)
            {
                string currFindName = ctrlName;
                Type findType = typeof(T);

                List<T> listFind= new List<T>();

                WinAPI.EnumChildWindows(_hWnd, new WinAPI.EnumWindowsProc((hWnd, lParam) =>
                                            {
                                                object findObj = null;
                                                EnumWindowsProc<T>(hWnd, lParam, currFindName, ref findObj);
                                                if (findObj != null)
                                                {
                                                    listFind.Add((T)findObj);
                                                }

                                                return true;
                                            }),
                                        (int)_hWnd);

                if (listFind.Count != 0)
                {
                    return listFind;

                }

                iCount++;

                Thread.Sleep(200);
            }

            throw new ArgumentException(string.Format("can not find control:[{0}], in window:[{1}]", ctrlName, _name));
        }

        
        public T1 GetByIndex<T1>(int indexBaseZero) where T1 : UIItem
        {
            int iCount = 0;
            while (iCount < 50)
            {
                Type findType = typeof(T1);
                object findObj = null;

                int currIndex = 0;
                WinAPI.EnumChildWindows(_hWnd, new WinAPI.EnumWindowsProc((hWnd, lParam) =>
                                        {
                                            return EnumWindowsProc<T1>(hWnd, lParam, indexBaseZero, ref currIndex, out findObj);
                                        }),
                                        (int)_hWnd);

                if (findObj != null)
                {
                    return (T1)findObj;

                }

                iCount++;

                Thread.Sleep(200);
            }

            throw new ArgumentException(string.Format("can not find control:[{0}], in window:[{1},{2}]", typeof(T1).Name, _name, _hWnd.ToString("X8")));
        }

        public Menu GetMenu(params string[] ctrlNames)
        {
            Menu[] menuInfos = WinAPI.GetSubMenus(this);

            Menu currMenu = null;
            foreach (string name in ctrlNames)
            {
                currMenu = menuInfos.Where(x => x.name == name).First();
                menuInfos = WinAPI.GetSubMenus(currMenu);
            }

            currMenu.hWndWin = _hWnd;
            return currMenu;
        }

        public Window(IntPtr hWnd, string name)
        {
            _hWnd = hWnd;
            _name = name;

            uint pid = 0;
            WinAPI.GetWindowThreadProcessId(hWnd, out pid);

            _process = Application.FindProcess((int)pid);

            Thread.Sleep(2000);

            //while (!WinAPI.IsWindowEnabled(_hWnd))
            //{
            //    Thread.Sleep(200);
            //}
        }
        
        public bool IsExit()
        {
            return !WinAPI.IsWindow(_hWnd);
        }

        public void WaitExit(int second=5*60, Action  action = null)
        {
            Log.INFO(string.Format("Waiting Window[{0},{1}] Exit", _name, _hWnd));

            for (int i = 0; i < second; i++)
            {
                if (!WinAPI.IsWindow(_hWnd))
                {
                    return;
                }

                if (action != null)
                {
                    action();
                }


                Application.WinTree newTree = _process.GetWindowTree();

                IntPtr exceptHwnd = newTree.Find((IntPtr hwnd)=>
                                {
                                    string str = WinAPI.GetWindowText(hwnd);
                                    if (str.ToLower().Contains("error") || str.ToLower().Contains("fail") || str.ToLower().Contains("warn") || str.ToLower().Contains("not able"))
                                    {
                                        return true;
                                    }

                                    return false;
                                });

                if(exceptHwnd != IntPtr.Zero)
                {
                    IntPtr defaultHwnd = newTree.Find((IntPtr hwnd) =>
                    {
                        string str = WinAPI.GetWindowText(hwnd);
                        if(str.Contains(" Defaulting to Generic profile"))
                        {
                            return true;
                        }

                        return false;
                    });
                    
                    if (defaultHwnd == IntPtr.Zero)
                    {
                        throw new Exception(string.Format("Find a error window:{0}, HWND:{1}", WinAPI.GetWindowText(exceptHwnd), exceptHwnd));
                    }
                }


                Thread.Sleep(1000);
            }

            throw new Exception(string.Format("Wait Window[{0},{1}] Exit Timeout!", _name, _hWnd));
        }

        public IntPtr hwnd
        {
            get
            {
                return _hWnd;
            }
        }

        private bool EnumWindowsProc<T>(IntPtr hWnd, int lParam, string currFindName, ref object findObj) where T : UIItem
        {

            findObj = null;
            
            Type type = typeof(T);
            MethodInfo method = type.GetMethod("CreateByName");
            if (method == null)
            {
                method = type.BaseType.GetMethod("CreateByName");
            }

            findObj = method.Invoke(null, new object[] { hWnd, (IntPtr)lParam, currFindName });
            if (findObj != null)
            {
                return false;
            }

            
            return true;
        }

        private bool EnumWindowsProc<T1>(IntPtr hWnd, int lParam, int indexBaseZero, ref int currIndex, out object findObj) where T1 : UIItem
        {
            findObj = null;

            Type type = typeof(T1);
            MethodInfo method = type.GetMethod("CreateByIndex");
            if (method == null)
            {
                method = type.BaseType.GetMethod("CreateByIndex");
            }

            object[] Params = new object[] { hWnd, (IntPtr)lParam, indexBaseZero, currIndex};

            findObj = method.Invoke(null, Params);
            currIndex = (int)Params[3];

            if (findObj != null)
            {
                return false;
            }
            return true;
        }

        public IntPtr _hWnd;
        public string _name;
        internal Application _process;
    }

}
