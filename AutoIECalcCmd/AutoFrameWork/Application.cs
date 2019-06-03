using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AutoIECalcCmd;

namespace AutoFrameWork
{
    class Application
    {
        public static Application Launch(string appPath)
        {
            try
            {
                Process p = new Process();// Process.Start(appPath);
                p.StartInfo = new ProcessStartInfo();
                p.StartInfo.FileName = appPath;
                p.Start();


                Log.INFO(string.Format("EXE Launch:[{0}], PID:[{1}]", appPath, p.Id.ToString("X8")));
                return new Application(p);
            }
            catch (Exception e)
            {
                throw new ArgumentException(string.Format("Launch app failed! path:[{0}], reason:[{1}]", appPath, e.Message));
            }
        }

        public static void Stop(string processName)
        {
            foreach(var app in Process.GetProcessesByName(processName))
            {
                app.Kill();
                app.WaitForExit();
            }
        }

        public static Application TryFindProcess(string processname, int timeout = 300)
        {
            int waitmill = 500;
            int max_waitcout = timeout * 1000 / waitmill;

            int waitcout = 0;
            while (true)
            {
                Process p = Process.GetProcessesByName(processname).SingleOrDefault();
                if (p == null)
                {
                    if (waitcout < max_waitcout)
                    {
                        waitcout++;
                        Thread.Sleep(waitmill);
                        continue;
                    }
                    else
                    {
                        return null;
                    }

                }

                Thread.Sleep(500);
                return new Application(p);
            }

        }

        public static Application FindProcess(string processname, int timeout = 300)
        {
            int waitmill = 500;
            int max_waitcout = timeout*1000 / waitmill;

            int waitcout = 0;
            while (true)
            {
                Process p = Process.GetProcessesByName(processname).SingleOrDefault();
                if (p == null)
                {
                    if(waitcout < max_waitcout)
                    {
                        waitcout++;
                        Thread.Sleep(waitmill);
                        continue;
                    }
                    else
                    {
                        throw new Exception("find process timeout, processname:" + processname);
                    }
                    
                }

                Thread.Sleep(500);
                return new Application(p);
            }

        }

        public static Application FindProcess(int pid)
        {
            Process p = Process.GetProcessById(pid);
            if (p == null)
            {
                return null;
            }
            return new Application(p);
        }

        internal void ShowWindows()
        {
            List<IntPtr> listWinHwnd = new List<IntPtr>();
            WinAPI.EnumWindows(new WinAPI.EnumWindowsProc((hWnd, lParam) =>
                                    {
                                        uint pid = 0;
                                        WinAPI.GetWindowThreadProcessId(hWnd, out pid);

                                        if (pid == _process.Id)
                                        {
                                            //listWinHwnd.Add(hWnd);
                                            WinAPI.ShowWindow(hWnd, WinAPI.WindowShowCmd.SW_SHOWNORMAL);
                                        }

                                        return true;
                                    }),
                                    (IntPtr)0);

            //WinTree root = new WinTree(IntPtr.Zero);

            foreach (IntPtr hWnd in listWinHwnd)
            {
                //root.subWin.Add(new WinTree(hWnd));

                WinAPI.EnumChildWindows(hWnd,
                                        new WinAPI.EnumWindowsProc((hChildWnd, lParam) =>
                                        {
                                            WinAPI.ShowWindow(hChildWnd, WinAPI.WindowShowCmd.SW_SHOWNORMAL);
                                            return true;
                                        }),
                                    0);
            }
        }

        //public Window MainWindow()
        //{
        //    return new Window(_process.MainWindowHandle, WinAPI.GetWindowText(_process.MainWindowHandle));
        //}


        public void Exit()
        {
            Log.INFO(string.Format("EXE Exit, PID:[{0}]", _process.Id.ToString("X8")));
            _process.Kill();
            _process.WaitForExit();
        }

        public Window FindWindow(string winText, int waitsecond = 90)
        {
            return FindWindow(By.Name(winText), waitsecond);

            //try
            //{
                
            //    Window findWindow = null;
            //    WaitUntil((String findName) =>
            //                 {
            //                     WinTree tree = GetWindowTree();

            //                     IntPtr exceptHwnd = tree.Find((IntPtr currhwnd) =>
            //                     {
            //                         string str = WinAPI.GetWindowText(currhwnd);
            //                         if (str.ToLower().Contains("error") || str.ToLower().Contains("fail") || str.ToLower().Contains("warn"))
            //                         {
            //                             if(str == findName)
            //                             {
            //                                 return false;
            //                             }

            //                             return true;
            //                         }

            //                         return false;
            //                     });
            //                     if(exceptHwnd != IntPtr.Zero)
            //                     {
            //                         throw new Exception("get error report: " + WinAPI.GetWindowText(exceptHwnd));
            //                     }

            //                     IntPtr hwnd = tree.Find(findName);
            //                     if(hwnd == IntPtr.Zero)
            //                     {
            //                         return false;
            //                     }
            //                     findWindow = (Window)Activator.CreateInstance(typeof(Window), hwnd, findName);
            //                     return true;
            //                 },
            //              winText,
            //              60);

            //    Log.INFO(string.Format("Finded Winow:[{0},{1}], in PID:[{2}]", winText, findWindow.hwnd.ToString("X8"), _process.Id.ToString("X8")));
            //    return findWindow;
            //}
            //catch (TimeoutException e)
            //{
            //    throw new ArgumentException(string.Format("can not find window:[{0}] in PID:[{1}]", winText, _process.Id.ToString("X8")));
            //}


        }

        public Window FindWindow(Selector selector, int waitsecond = 90)
        {
            try
            {

                Window findWindow = null;
                WaitUntil((String findName) =>
                            {
                                WinTree tree = GetWindowTree();

                                IntPtr exceptHwnd = tree.Find((IntPtr currhwnd) =>
                                                                {
                                                                    string str = WinAPI.GetWindowText(currhwnd);
                                                                    if (str.ToLower().Contains("fail") || str.ToLower().Contains("warn") || str.ToLower().Contains("not able"))
                                                                    {
                                                                        if (selector.IsTrue(currhwnd))
                                                                        {
                                                                            return false;
                                                                        }

                                                                        return true;
                                                                    }

                                                                    return false;
                                                                });
                                if (exceptHwnd != IntPtr.Zero)
                                {
                                    throw new Exception("get error report: " + WinAPI.GetWindowText(exceptHwnd));
                                }

                                IntPtr hwnd = tree.Find((IntPtr currhwnd) =>
                                                        {
                                                            return selector.IsTrue(currhwnd);
                                                        });
                                if (hwnd == IntPtr.Zero)
                                {
                                    return false;
                                }
                                findWindow = (Window)Activator.CreateInstance(typeof(Window), hwnd, WinAPI.GetWindowText(hwnd));
                                return true;
                            },
                          "",
                          waitsecond);

                Log.INFO(string.Format("Finded Winow:[{0},{1}], in PID:[{2}]", findWindow._name, findWindow.hwnd.ToString("X8"), _process.Id.ToString("X8")));
                return findWindow;
            }
            catch (TimeoutException e)
            {
                throw new ArgumentException(string.Format(" can not find window:[{0}] in PID:[{1}]", selector.desc(), _process.Id.ToString("X8")));
            }
        }

        //public Window FindWindowContains(string winText)
        //{
        //    try
        //    {

        //        Window findWindow = null;

        //        WaitUntil((String findName) =>
        //        {
        //            WinAPI.EnumWindows(new WinAPI.EnumWindowsProc((hWnd, lParam) =>
        //            {
        //                uint pid = 0;
        //                WinAPI.GetWindowThreadProcessId(hWnd, out pid);

        //                if (pid != _process.Id)
        //                {
        //                    return true;
        //                }

        //                int length = WinAPI.GetWindowTextLength(hWnd);
        //                StringBuilder windowName = new StringBuilder(length + 1);
        //                WinAPI.GetWindowText(hWnd, windowName, windowName.Capacity);

        //                if (windowName.ToString().Replace("&", "").Contains(findName))
        //                {
        //                    findWindow = (Window)Activator.CreateInstance(typeof(Window), hWnd, findName);
        //                    return false;
        //                }

        //                return true;
        //            }),
        //                            (IntPtr)0);

        //            if (findWindow != null)
        //            {
        //                return true;
        //            }

        //            return false;
        //        },
        //                   winText,
        //                   60);

        //        Log.INFO(string.Format("Finded Winow:[{0},{1}], in PID:[{2}]", winText, findWindow.hwnd.ToString("X8"), _process.Id.ToString("X8")));
        //        return findWindow;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ArgumentException(string.Format("can not find window:[{0}] in PID:[{1}]", winText, _process.Id.ToString("X8")));
        //    }
        //}

        public Window TryFindWindow(string winText)
        {
            try
            {

                Window findWindow = null;

                WaitUntil((String findName) =>
                            {
                                WinAPI.EnumWindows(new WinAPI.EnumWindowsProc((hWnd, lParam) =>
                                {
                                    uint pid = 0;
                                    WinAPI.GetWindowThreadProcessId(hWnd, out pid);

                                    if (pid != _process.Id)
                                    {
                                        return true;
                                    }

                                    int length = WinAPI.GetWindowTextLength(hWnd);
                                    StringBuilder windowName = new StringBuilder(length + 1);
                                    WinAPI.GetWindowText(hWnd, windowName, windowName.Capacity);

                                    if (windowName.ToString().Replace("&", "") == findName)
                                    {
                                        findWindow = (Window)Activator.CreateInstance(typeof(Window), hWnd, findName);
                                        return false;
                                    }

                                    return true;
                                }),
                               (IntPtr)0);

                                if (findWindow != null)
                                {
                                    return true;
                                }

                                return false;
                            },
                           winText,
                           5);

                Log.INFO(string.Format("Finded Winow:[{0},{1}], in PID:[{2}]", winText, findWindow.hwnd.ToString("X8"), _process.Id.ToString("X8")));
                return findWindow;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //internal Window FindChildWindow(string v1)
        //{
        //    int iCount = 0;
        //    while (iCount < 100)
        //    {
        //        WinTree winTree = GetWindowTree();
        //        IntPtr hWnd  = winTree.Find(v1);
        //        if (hWnd != IntPtr.Zero)
        //        {
        //            Window findWindow = (Window)Activator.CreateInstance(typeof(Window), hWnd, v1);
        //            Log.INFO(string.Format("Finded chid Window:[{0},{1}], in process:[{2}]", findWindow._name, findWindow._hWnd.ToString("X8"), _process.Id));
        //            return findWindow;
        //        }

        //        iCount++;

        //        Thread.Sleep(200);
        //    }

        //    throw new ArgumentException(string.Format("can not find child window:[{0}], in process:[{1}]", v1, _process.Id));
        //}

        internal void FindChildWindow(string v1, Action<Window> actionNomarl, string v2, Action<Window> actionException)
        {
            int iCount = 0;
            while (iCount < 100)
            {
                WinTree winTree = GetWindowTree();
                IntPtr hWnd = winTree.Find(v2);
                if(hWnd != IntPtr.Zero && WinAPI.IsWindowEnabled(hWnd))
                {
                    Window findWindow = (Window)Activator.CreateInstance(typeof(Window), hWnd, v2);
                    Log.INFO(string.Format("Finded chid Window:[{0},{1}], in process:[{2}]", findWindow._name, findWindow._hWnd.ToString("X8"), _process.Id));

                    actionException(findWindow);
                    return;
                }

                hWnd = winTree.Find(v1);
                if (hWnd != IntPtr.Zero && WinAPI.IsWindowEnabled(hWnd))
                {
                    Window findWindow = (Window)Activator.CreateInstance(typeof(Window), hWnd, v1);
                    Log.INFO(string.Format("Finded chid Window:[{0},{1}], in process:[{2}]", findWindow._name, findWindow._hWnd.ToString("X8"), _process.Id));

                    actionNomarl(findWindow);
                    return;
                }

                iCount++;

                Thread.Sleep(200);
            }

            throw new ArgumentException(string.Format("can not find child window:[{0}], in process:[{1}]", v1 + "," + v2, _process.Id));
        }

        public class WinTree
        {
            public WinTree(IntPtr hWnd)
            {
                this.hWnd = hWnd;
            }

            public WinTree Find(IntPtr hWnd)
            {
                if(this.hWnd == hWnd)
                {
                    return this;
                }

                foreach(WinTree tree in subWin)
                {
                    WinTree find = tree.Find(hWnd);
                    if (find != null)
                    {
                        return find;
                    }
                }

                return null;
            }

            public IntPtr Find(string name)
            {
                int length = WinAPI.GetWindowTextLength(hWnd);
                StringBuilder windowName = new StringBuilder(length + 1);
                WinAPI.GetWindowText(this.hWnd, windowName, windowName.Capacity);

                if (windowName.ToString() == name)
                {
                    return this.hWnd;
                }

                foreach (WinTree tree in subWin)
                {
                    IntPtr find = tree.Find(name);
                    if (find != IntPtr.Zero)
                    {
                        return find;
                    }
                }

                return IntPtr.Zero;
            }

            public IntPtr Find(Func<IntPtr, bool> match)
            {
                if (match(hWnd))
                {
                    return this.hWnd;
                }

                foreach (WinTree tree in subWin)
                {
                    IntPtr find = tree.Find(match);
                    if (find != IntPtr.Zero)
                    {
                        return find;
                    }
                }

                return IntPtr.Zero;
            }

            public override string ToString()
            {
                string tab = "";
                return StringFormat(ref tab);
            }

            private string StringFormat(ref string tab)
            {
                string rslt = "";
                if (hWnd != IntPtr.Zero)
                {
                    int length = WinAPI.GetWindowTextLength(hWnd);
                    StringBuilder windowName = new StringBuilder(length + 1);
                    WinAPI.GetWindowText(hWnd, windowName, windowName.Capacity);

                    string winClass = WinAPI.GetWinClass(hWnd);

                    uint pid = 0;
                    WinAPI.GetWindowThreadProcessId(hWnd, out pid);

                    rslt += string.Format("HWND:{0}, WinText:{1}, ClassType:{2}, ProcessID:{3}", hWnd.ToString("X8"), windowName.ToString(), winClass, pid.ToString("X8")) + "\n";
                }

                tab += "    ";
                foreach (WinTree tree in subWin)
                {
                    rslt += tab + tree.StringFormat(ref tab);
                }

                tab = tab.Substring(0, tab.Length - 4);

                return rslt;
            }

            public IntPtr hWnd;
            public List<WinTree> subWin = new List<WinTree>();

            //internal string[] Except(WinTree startTree)
            //{
            //    foreach(var elem in subWin)
            //    {
            //        if(startTree.subWin.Contains)
            //    }
            //}
        }

        public WinTree GetWindowTree()
        {
            List<IntPtr> listWinHwnd = new List<IntPtr>();
            WinAPI.EnumWindows(new WinAPI.EnumWindowsProc((hWnd, lParam) =>
            {
                uint pid = 0;
                WinAPI.GetWindowThreadProcessId(hWnd, out pid);

                if (pid == _process.Id)
                {
                    listWinHwnd.Add(hWnd);
                }

                return true;
            }),
                                    (IntPtr)0);

            WinTree root = new WinTree(IntPtr.Zero);

            foreach (IntPtr hWnd in listWinHwnd)
            {
                root.subWin.Add(new WinTree(hWnd));

                WinAPI.EnumChildWindows(hWnd,
                                        new WinAPI.EnumWindowsProc((hChildWnd, lParam) =>
                                        {
                                            IntPtr parenthWind = WinAPI.GetParent(hChildWnd);
                                            root.Find(parenthWind).subWin.Add(new WinTree(hChildWnd));
                                            return true;
                                        }),
                                    0);
            }

            return root;
        }

        //public string GetWindowTree()
        //{
        //    List<IntPtr> listWinHwnd = new List<IntPtr>();
        //    WinAPI.EnumWindows(new WinAPI.EnumWindowsProc((hWnd, lParam) =>
        //                            {
        //                                uint pid = 0;
        //                                WinAPI.GetWindowThreadProcessId(hWnd, out pid);

        //                                if (pid == _process.Id)
        //                                {
        //                                    listWinHwnd.Add(hWnd);
        //                                }

        //                                return true;
        //                            }),
        //                            (IntPtr)0);

        //    WinTree root = new WinTree(IntPtr.Zero);

        //    foreach (IntPtr hWnd in listWinHwnd)
        //    {
        //        root.subWin.Add(new WinTree(hWnd));

        //        WinAPI.EnumChildWindows(hWnd,
        //                                new WinAPI.EnumWindowsProc((hChildWnd, lParam) =>
        //                                {
        //                                    IntPtr parenthWind = WinAPI.GetParent(hChildWnd);
        //                                    root.Find(parenthWind).subWin.Add(new WinTree(hChildWnd));
        //                                    return true;
        //                                }),
        //                            0);
        //    }

        //    return string.Format("process:[{0}] win tree:\n{1}", _process.Id.ToString("X8"), root.ToString());
        //}

        private Application(Process p)
        {
            _process = p;
        }

        void WaitUntil<TParam>(Func<TParam, bool> action, TParam param, int timeoutSec)
        {
            int timePer = 200;

            int iCount = 0;
            while (iCount < timeoutSec * 1000 / timePer)
            {
                if (action(param))
                {
                    return;
                }
                iCount++;
                Thread.Sleep(timePer);
            }

            throw new TimeoutException();
        }

        Process _process;
        
    }
}
