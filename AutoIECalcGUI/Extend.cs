using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AutoIECalcGUI
{
    public static class  Extend
    {
        private const int EM_SETCUEBANNER = 0x1501;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]

        private static extern Int32 SendMessage
         (IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        /// <summary>
        /// 为TextBox设置水印文字
        /// </summary>
        /// <param name="textBox">TextBox</param>
        /// <param name="watermark">水印文字</param>
        public static void SetWatermark(this TextBox textBox, string watermark)
        {
            textBox.LostFocus += new System.EventHandler(delegate (Object o, EventArgs e) 
                                                        {
                                                            if(textBox.Text.Length == 0)
                                                            {
                                                                SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, watermark);
                                                            }
                                                        });
            textBox.GotFocus += new System.EventHandler(delegate (Object o, EventArgs e)
                                                        {
                                                            SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, string.Empty);
                                                        });

        }
    }
}
