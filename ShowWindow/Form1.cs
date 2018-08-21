using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowWindow
{
    public partial class Form1 : Form
    {
        public enum WindowShowCmd
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_MINIMIZE = 6,
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ShowWindow(IntPtr hWnd, WindowShowCmd nCmdShow);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowWindow((IntPtr)Convert.ToInt32(textBox1.Text, 16), WindowShowCmd.SW_SHOWNORMAL);
        }
    }
}
