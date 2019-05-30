using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using AutoIECalcCmd;

namespace AutoFrameWork
{
    public class WinAPI
    {
        public enum WMMessage
        {
            WM_KEYDOWN = 0x0100,
            WM_KEYUP = 0x101,
            WM_SETTEXT = 0x000c,
            WM_GETTEXT = 0x000D,
            WM_GETTEXTLENGTH = 0x000E,
            WM_COMMAND = 0x0111,
            WM_LBUTTONDOWN = 0x201,
            WM_LBUTTONUP = 0x202
        }

        public enum VirtualKeyCode
        {
            VK_CONTROL = 0x11,
            VK_MENU = 0x12
        }

        public enum WindowShowCmd
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_MINIMIZE = 6,
        }

        public enum ListViewMessages
        {
            HDM_GETITEMCOUNT = 0x1200,
            LVM_GETHEADER = 0x101F,
            LVM_GETITEMTEXT = 0x104B,
            LVM_SETITEMSTATE = 0x102B,
            LVM_GETITEMCOUNT = 0x1004,
        }
        
        public enum ListBoxMessages
        {
            LBN_SELCHANGE = 0x001,
            LB_SETSEL = 0x0185,
            LB_GETCURSEL = 0x0188,
            LB_GETTEXT = 0x0189,
            LB_GETCOUNT = 0x018B,
            LB_SETCURSEL = 0x0186,
            LB_SELECTSTRING = 0x018C,
        }
        

        public enum ButtonMessages
        {
            BM_SETCHECK = 0x00F1,
            BM_CLICK = 0x00F5,

            BST_CHECKED = 0x0001,
        }

        public enum ListViewItemFilters : uint
        {
            LVIF_TEXT = 0x0001,
            LVCF_TEXT = 0x00000004
        }

        public enum ToolbarMessage
        {
            TB_GETBUTTON = 0x417,
            TB_GETBUTTONTEXT = 0x44b,

        }

        public enum ComboBoxMessage
        {
            CN_COMMAND = 0xBD11,
            CBN_SELCHANGE = 0x0001,
            CB_SELECTSTRING = 0x014D,
            CB_SETCURSEL    = 0x014E,
            CB_FINDSTRING   = 0x014C,
            CB_FINDSTRINGEXACT = 0x158
        }

        public enum TabCtrlMessage
        {
            TCM_FIRST	= 0x1300,
            TCM_GETITEMCOUNT = TCM_FIRST + 4,
            TCM_SETCURFOCUS = TCM_FIRST + 48,
            TCM_GETITEMA = TCM_FIRST + 60,
            TCM_SETCURSEL = TCM_FIRST+12,
        }

        public enum ProgressBarMessage
        {
            PBM_GETPOS = 0x0400 + 8,
        }

        [Flags]
        public enum ProcessAccessFlags : int
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }

        [Flags]
        public enum MemoryProtection
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }

        internal enum INPUT_TYPE : uint
        {
            INPUT_MOUSE = 0,
            INPUT_KEYBOARD = 1,
            INPUT_HARDWARE = 2
        }

        [Flags]
        public enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }

        [Flags]
        public enum MIIM
        {
            BITMAP = 0x00000080,
            CHECKMARKS = 0x00000008,
            DATA = 0x00000020,
            FTYPE = 0x00000100,
            ID = 0x00000002,
            STATE = 0x00000001,
            STRING = 0x00000040,
            SUBMENU = 0x00000004,
            TYPE = 0x00000010
        }

        public enum GWL
        {
            GWL_WNDPROC = (-4),
            GWL_HINSTANCE = (-6),
            GWL_HWNDPARENT = (-8),
            GWL_STYLE = (-16),
            GWL_EXSTYLE = (-20),
            GWL_USERDATA = (-21),
            GWL_ID = (-12)
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct LVITEM
        {
            public uint mask;
            public int iItem;
            public int iSubItem;
            public uint state;
            public uint stateMask;
            public IntPtr pszText;
            public int cchTextMax;
            public int iImage;
            public IntPtr lParam;

            public enum STATE
            {
                LVIS_FOCUSED = 0x0001,
                LVIS_SELECTED = 0x0002,
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TBBUTTON
        {
            public Int32 iBitmap;
            public Int32 idCommand;
            public byte fsState;
            public byte fsStyle;
            public byte bReserved1;
            public byte bReserved2;
            public UInt32 dwData;
            public IntPtr iString;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct TCITEM
        {
            public uint mask;
            public int state;
            public int statemask;
            public IntPtr text;
            public int size;
            public int image;
            public int param;

            public enum Filters
            {
                TCIF_TEXT = 0x01,
            }
        }

        public struct KEYBOARDINPUT
        {
            public uint type;
            public ushort wVk;
            ushort wScan;
            public uint dwFlags;
            uint time;
            uint dwExtraInfo;
            uint unused1;
            uint unused2;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MENUITEMINFO
        {
            public Int32 cbSize = Marshal.SizeOf(typeof(MENUITEMINFO));
            public MIIM fMask;
            public UInt32 fType;
            public UInt32 fState;
            public UInt32 wID;
            public IntPtr hSubMenu;
            public IntPtr hbmpChecked;
            public IntPtr hbmpUnchecked;
            public IntPtr dwItemData = IntPtr.Zero;
            public IntPtr dwTypeData = IntPtr.Zero;
            public UInt32 cch; // length of dwTypeData
            public IntPtr hbmpItem;

            public MENUITEMINFO() { }
            public MENUITEMINFO(MIIM pfMask)
            {
                fMask = pfMask;
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ShowWindow(IntPtr hWnd, WindowShowCmd nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr Param, string s);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr Param, StringBuilder s);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32 ")]
        public static extern IntPtr EnumChildWindows(IntPtr hWndParent, EnumWindowsProc lpEnumFunc, int lParam);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        //消息发送API
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern IntPtr VirtualAllocEx(IntPtr hwnd, IntPtr lpaddress, int size, AllocationType type, MemoryProtection protect);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        byte[] lpBuffer,
        Int32 nSize,
        out int lpNumberOfBytesWritten);
        [DllImport("kernel32.dll")]
        public static extern int GetProcAddress(int hwnd, string lpname);
        [DllImport("kernel32.dll")]
        public static extern int GetModuleHandleA(string name);
        [DllImport("kernel32.dll")]
        public static extern int CreateRemoteThread(IntPtr hwnd, int attrib, int size, int address, int par, int flags, int threadid);


        [DllImport("Kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll ")]
        public static extern bool CloseHandle(IntPtr hProcess);

        [DllImport("coredll.dll", SetLastError = true)]
        public static extern uint SendInput(uint cInputs, /* [MarshalAs(UnmanagedType.LPArray)] */ KEYBOARDINPUT[] inputs, int cbSize);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, AllocationType dwFreeType);

        [DllImport("user32.dll")]
        public static extern IntPtr GetTopWindow(IntPtr hWnd);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindow(IntPtr hWnd);
        
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowEnabled(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll")]
        public static extern IntPtr GetMenu(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMenuItemInfo(IntPtr hMenu, UInt32 uItem, bool fByPosition, [In, Out] MENUITEMINFO lpmii);

        [DllImport("user32.dll")]
        public static extern uint GetMenuItemID(IntPtr hMenu, int nPos);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        public delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

        public static unsafe bool GetToolbarButton(IntPtr hToolbar, int i, ref TBBUTTON tbButton)
        {
            // One page
            const int BUFFER_SIZE = 0x1000;

            byte[] localBuffer = new byte[BUFFER_SIZE];

            UInt32 processId = 0;
            UInt32 threadId = WinAPI.GetWindowThreadProcessId(hToolbar, out processId);

            IntPtr hProcess = WinAPI.OpenProcess((int)WinAPI.ProcessAccessFlags.All, false, processId);
            if (hProcess == IntPtr.Zero) 
            {
                throw new ArgumentException("OpenProcess failed");
            }

            IntPtr ipRemoteBuffer = WinAPI.VirtualAllocEx(hProcess, IntPtr.Zero, BUFFER_SIZE,  WinAPI.AllocationType.Commit, WinAPI.MemoryProtection.ExecuteReadWrite);
            if (ipRemoteBuffer == IntPtr.Zero)
            {
                throw new ArgumentException("VirtualAllocEx failed");
            }

            // TBButton
            fixed (TBBUTTON* pTBButton = &tbButton)
            {
                IntPtr ipTBButton = new IntPtr(pTBButton);

                int b = (int)WinAPI.SendMessage(hToolbar, (int)WinAPI.ToolbarMessage.TB_GETBUTTON, (IntPtr)i, ipRemoteBuffer);
                if (b == 0) 
                {
                    throw new ArgumentException("SendMessage TB_GETBUTTON failed");
                }

                // this is fixed
                int ipBytesRead = 0;

                bool b2 = WinAPI.ReadProcessMemory(hProcess, ipRemoteBuffer, ipTBButton, sizeof(TBBUTTON), out ipBytesRead);
                if (!b2) 
                {
                    throw new ArgumentException("ReadProcessMemory failed");
                }
            }

            WinAPI.VirtualFreeEx(
                hProcess,
                ipRemoteBuffer,
                0,
                WinAPI.AllocationType.Release);

            WinAPI.CloseHandle(hProcess);

            return true;
        }

        internal static string GetWinClass(IntPtr hWnd)
        {
            StringBuilder className = new StringBuilder(1000);
            WinAPI.GetClassName(hWnd, className, className.Capacity);

            return className.ToString();
        }

        internal static int GetListViewItemNum(IntPtr hWnd)
        {
            IntPtr iItem = SendMessage(hWnd, (int)ListViewMessages.LVM_GETITEMCOUNT, (IntPtr)0, (IntPtr)0);
            return (int)iItem;
        }

        internal static int GetListViewColumnNum(IntPtr hWnd)
        {
            IntPtr hWndHdr = WinAPI.SendMessage(hWnd, (int)WinAPI.ListViewMessages.LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);
            int count = (int)SendMessage(hWndHdr, (int)WinAPI.ListViewMessages.HDM_GETITEMCOUNT, IntPtr.Zero, IntPtr.Zero);
            return count;
        }

        internal static string GetListViewItemText(IntPtr hWnd, int i)
        {
            int columCount = GetListViewColumnNum(hWnd);

            string result = "";
            for (int j=0; j< columCount; j++ )
            {

                const int dwBufferSize = 2048;
                int bytesWrittenOrRead = 0;
                WinAPI.LVITEM lvCol;
                string retval;
                bool bSuccess;
                System.IntPtr hProcess = System.IntPtr.Zero;
                System.IntPtr lpRemoteBuffer = System.IntPtr.Zero;
                System.IntPtr lpLocalBuffer = System.IntPtr.Zero;

                try
                {
                    uint processid = 0;
                    WinAPI.GetWindowThreadProcessId(hWnd, out processid);

                    lvCol = new WinAPI.LVITEM();
                    lpLocalBuffer = System.Runtime.InteropServices.Marshal.AllocHGlobal(dwBufferSize);
                    hProcess = WinAPI.OpenProcess((int)WinAPI.ProcessAccessFlags.All, false, processid);
                    if (hProcess == System.IntPtr.Zero)
                        throw new System.ApplicationException("Failed to access process!");

                    lpRemoteBuffer = WinAPI.VirtualAllocEx(hProcess, IntPtr.Zero, dwBufferSize, WinAPI.AllocationType.Commit, WinAPI.MemoryProtection.ExecuteReadWrite);
                    if (lpRemoteBuffer == System.IntPtr.Zero)
                        throw new System.SystemException("Failed to allocate memory in remote process");

                    lvCol.mask = (int)WinAPI.ListViewItemFilters.LVIF_TEXT;
                    lvCol.pszText = (System.IntPtr)(lpRemoteBuffer.ToInt32() + System.Runtime.InteropServices.Marshal.SizeOf(typeof(WinAPI.LVITEM)));
                    lvCol.cchTextMax = 500;
                    lvCol.iItem = i;
                    lvCol.iSubItem = j;

                    var size = Marshal.SizeOf(lvCol);
                    // Both managed and unmanaged buffers required.
                    var bytes = new byte[size];
                    var ptr = Marshal.AllocHGlobal(size);
                    // Copy object byte-to-byte to unmanaged memory.
                    Marshal.StructureToPtr(lvCol, ptr, false);
                    // Copy data from unmanaged memory to managed buffer.
                    Marshal.Copy(ptr, bytes, 0, size);
                    // Release unmanaged memory.
                    //Marshal.FreeHGlobal(ptr);

                    bSuccess = WinAPI.WriteProcessMemory(hProcess, lpRemoteBuffer, bytes, System.Runtime.InteropServices.Marshal.SizeOf(typeof(WinAPI.LVITEM)), out bytesWrittenOrRead);
                    if (!bSuccess)
                        throw new System.SystemException("Failed to write to process memory");


                    WinAPI.SendMessage(hWnd, (int)WinAPI.ListViewMessages.LVM_GETITEMTEXT, (System.IntPtr)0, lpRemoteBuffer);

                    bSuccess = WinAPI.ReadProcessMemory(hProcess, lpRemoteBuffer, lpLocalBuffer, dwBufferSize, out bytesWrittenOrRead);

                    if (!bSuccess)
                        throw new System.SystemException("Failed to read from process memory");

                    retval = System.Runtime.InteropServices.Marshal.PtrToStringUni((System.IntPtr)(lpLocalBuffer.ToInt32() + System.Runtime.InteropServices.Marshal.SizeOf(typeof(WinAPI.LVITEM))));
                }
                finally
                {
                    if (lpLocalBuffer != System.IntPtr.Zero)
                        System.Runtime.InteropServices.Marshal.FreeHGlobal(lpLocalBuffer);
                    if (lpRemoteBuffer != System.IntPtr.Zero)
                        WinAPI.VirtualFreeEx(hProcess, lpRemoteBuffer, 0, WinAPI.AllocationType.Release);
                    if (hProcess != System.IntPtr.Zero)
                        WinAPI.CloseHandle(hProcess);
                }

                result += " | " + retval;
            }

            return result;
        }

        internal static int GetListBoxItemNum(IntPtr hWnd)
        {
            IntPtr iItem = SendMessage(hWnd, (int)ListBoxMessages.LB_GETCOUNT, (IntPtr)0, (IntPtr)0);
            return (int)iItem;
        }

        internal static string GetListBoxItemText(IntPtr hWnd, int i)
        {
            StringBuilder sb = new StringBuilder();
            int size = SendMessage(hWnd, (int)WinAPI.ListBoxMessages.LB_GETTEXT, (System.IntPtr)i, sb);
            return sb.ToString();
        }

        internal static int GetTabCtrlItemNum(IntPtr _hWnd)
        {
            IntPtr iItem = SendMessage(_hWnd, (int)TabCtrlMessage.TCM_GETITEMCOUNT, (IntPtr)0, (IntPtr)0);
            return (int)iItem;
        }

        internal static string GetTabCtrlItemText(IntPtr _hWnd, int i)
        {
            const int dwBufferSize = 2048;
            int bytesWrittenOrRead = 0;
            WinAPI.TCITEM lvCol;
            string retval;
            bool bSuccess;
            System.IntPtr hProcess = System.IntPtr.Zero;
            System.IntPtr lpRemoteBuffer = System.IntPtr.Zero;
            System.IntPtr lpLocalBuffer = System.IntPtr.Zero;

            try
            {
                uint processid = 0;
                WinAPI.GetWindowThreadProcessId(_hWnd, out processid);

                lvCol = new WinAPI.TCITEM();
                lpLocalBuffer = System.Runtime.InteropServices.Marshal.AllocHGlobal(dwBufferSize);
                hProcess = WinAPI.OpenProcess((int)WinAPI.ProcessAccessFlags.All, false, processid);
                if (hProcess == System.IntPtr.Zero)
                    throw new System.ApplicationException("Failed to access process!");

                lpRemoteBuffer = WinAPI.VirtualAllocEx(hProcess, IntPtr.Zero, dwBufferSize, WinAPI.AllocationType.Commit, WinAPI.MemoryProtection.ExecuteReadWrite);
                if (lpRemoteBuffer == System.IntPtr.Zero)
                    throw new System.SystemException("Failed to allocate memory in remote process");

                lvCol.mask = (int)WinAPI.TCITEM.Filters.TCIF_TEXT;
                lvCol.text = (System.IntPtr)(lpRemoteBuffer.ToInt32() + System.Runtime.InteropServices.Marshal.SizeOf(typeof(WinAPI.TCITEM)));
                lvCol.size = dwBufferSize;
                var size = Marshal.SizeOf(lvCol);
                // Both managed and unmanaged buffers required.
                var bytes = new byte[size];
                var ptr = Marshal.AllocHGlobal(size);
                // Copy object byte-to-byte to unmanaged memory.
                Marshal.StructureToPtr(lvCol, ptr, false);
                // Copy data from unmanaged memory to managed buffer.
                Marshal.Copy(ptr, bytes, 0, size);
                // Release unmanaged memory.
                //Marshal.FreeHGlobal(ptr);

                bSuccess = WinAPI.WriteProcessMemory(hProcess, lpRemoteBuffer, bytes, System.Runtime.InteropServices.Marshal.SizeOf(typeof(WinAPI.TCITEM)), out bytesWrittenOrRead);
                if (!bSuccess)
                    throw new System.SystemException("Failed to write to process memory");


                WinAPI.SendMessage(_hWnd, (int)WinAPI.TabCtrlMessage.TCM_GETITEMA, (System.IntPtr)i, lpRemoteBuffer);

                bSuccess = WinAPI.ReadProcessMemory(hProcess, lpRemoteBuffer, lpLocalBuffer, dwBufferSize, out bytesWrittenOrRead);

                if (!bSuccess)
                    throw new System.SystemException("Failed to read from process memory");

                retval = System.Runtime.InteropServices.Marshal.PtrToStringUni((System.IntPtr)(lpLocalBuffer.ToInt32() + System.Runtime.InteropServices.Marshal.SizeOf(typeof(WinAPI.TCITEM))));
            }
            finally
            {
                if (lpLocalBuffer != System.IntPtr.Zero)
                    System.Runtime.InteropServices.Marshal.FreeHGlobal(lpLocalBuffer);
                if (lpRemoteBuffer != System.IntPtr.Zero)
                    WinAPI.VirtualFreeEx(hProcess, lpRemoteBuffer, 0, WinAPI.AllocationType.Release);
                if (hProcess != System.IntPtr.Zero)
                    WinAPI.CloseHandle(hProcess);
            }

            return retval;            
        }

        internal static Menu[] GetSubMenus(Window window)
        {
            return GetMenuArray(GetMenu(window.hwnd));
        }

        internal static Menu[] GetSubMenus(Menu parent)
        {
            Menu[] menus = GetMenuArray(parent.hWndSub);
            foreach(Menu menu in menus)
            {
                menu.parent = parent;
            }

            return menus;
        }

        internal static Menu[] GetMenuArray(IntPtr mHwnd)
        {
            List<Menu> list = new List<Menu>();

            int count = WinAPI.GetMenuItemCount(mHwnd);
            for (uint i = 0; i < count; i++)
            {

                WinAPI.MENUITEMINFO mif = new WinAPI.MENUITEMINFO(WinAPI.MIIM.STRING | WinAPI.MIIM.ID | WinAPI.MIIM.SUBMENU);
                if (!WinAPI.GetMenuItemInfo(mHwnd, i, true, mif))
                {
                    throw new Exception(string.Format("WinAPI.GetMenuItemInfo failed! index:[{0}]", i));
                }

                mif.cch++;
                mif.dwTypeData = Marshal.AllocHGlobal((IntPtr)(mif.cch * 2));
                try
                {
                    if (!WinAPI.GetMenuItemInfo(mHwnd, i, true, mif))
                    {
                        throw new Exception(string.Format("WinAPI.GetMenuItemInfo failed! index:[{0}]", i));
                    }
                    string caption = Marshal.PtrToStringUni(mif.dwTypeData); ;

                    
                    list.Add(new Menu(GetMenuItemID(mHwnd, (int)i), mHwnd, mif.hSubMenu, IntPtr.Zero, caption.Replace("&", "")));
                }
                finally
                {
                    Marshal.FreeHGlobal(mif.dwTypeData);
                }
            }
            return list.ToArray();
        }

        internal static int MakeWParam(int loWord, int hiWord)
        {
            return (loWord & 0xFFFF) + ((hiWord & 0xFFFF) << 16);
        }

        public static string GetWindowText(IntPtr hwnd)
        {
            int length = WinAPI.GetWindowTextLength(hwnd);
            StringBuilder windowName = new StringBuilder(length + 1);
            WinAPI.GetWindowText(hwnd, windowName, windowName.Capacity);

            return windowName.ToString();
        }
    }
}
