using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using AutoIECalcCmd;

namespace AutoFrameWork
{
    class ListItem : UIItem
    {
        public ListItem(IntPtr hWnd, IntPtr hWndWin, string ctrlName, int rowId) : base(hWnd, hWndWin, ctrlName, rowId)
        {

        }

        public static ListItem CreateByName(IntPtr hWnd, IntPtr hWndWin, String name)
        {
            int rowId = -1;
            if (CheckByName(hWnd, name, ref rowId))
            {
                return (ListItem)Activator.CreateInstance(typeof(ListItem), hWnd, hWndWin, name, rowId);
            }

            return null;
        }

        public void Click()
        {
            Log.INFO(string.Format("Click ListItem:[{0}, {1}] win[{2}] ", _Text, _hWnd.ToString("X8"), _hWndWin.ToString("X8")));

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
                WinAPI.GetWindowThreadProcessId(_hWnd, out processid);

                lvCol = new WinAPI.LVITEM();
                lpLocalBuffer = System.Runtime.InteropServices.Marshal.AllocHGlobal(dwBufferSize);
                hProcess = WinAPI.OpenProcess((int)WinAPI.ProcessAccessFlags.All, false, processid);
                if (hProcess == System.IntPtr.Zero)
                    throw new System.ApplicationException("Failed to access process!");

                lpRemoteBuffer = WinAPI.VirtualAllocEx(hProcess, IntPtr.Zero, dwBufferSize, WinAPI.AllocationType.Commit, WinAPI.MemoryProtection.ExecuteReadWrite);
                if (lpRemoteBuffer == System.IntPtr.Zero)
                    throw new System.SystemException("Failed to allocate memory in remote process");

                lvCol.iItem = (int)_param;
                lvCol.iSubItem = 0;
                lvCol.stateMask = (uint)(WinAPI.LVITEM.STATE.LVIS_SELECTED | WinAPI.LVITEM.STATE.LVIS_FOCUSED);
                lvCol.state = lvCol.stateMask;
                lvCol.iSubItem = 0;
                lvCol.mask = 0x0008;
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


                int rowId = (int)_param;
                WinAPI.SendMessage(_hWnd, (int)WinAPI.ListViewMessages.LVM_SETITEMSTATE, new IntPtr(rowId), lpRemoteBuffer);

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
        }

        private static bool CheckByName(IntPtr hWnd, String name, ref int rowId)
        {
            string winClass = WinAPI.GetWinClass(hWnd);
            if(winClass != "SysListView32")
            {
                return false;
            }

            //Console.WriteLine(string.Format("{0} {1}", hWnd.ToString("x8"), winClass));

            int rowNum = WinAPI.GetListViewItemNum(hWnd);
            for(int i=0; i<rowNum; i++)
            {
                string strText = WinAPI.GetListViewItemText(hWnd, i);
                if(strText.Contains(name))
                {
                    rowId = i;
                    return true;
                }
            }

            return false;
        }
    }
}
