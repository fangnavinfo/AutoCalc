using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AutoIECalcPublic;

namespace AutoIECalcGUI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Program.Config = ConfigSetting.Load(ConfigSetting.WeiyaConfigPath);
            Application.Run(new WeiyaCalcForm());
        }

        public static ConfigSetting Config = null; // ConfigSetting.Load(System.AppDomain.CurrentDomain.BaseDirectory + "weiyaconfig.json");
        //public static ConfigSetting HadConfig = ConfigSetting.Load(System.AppDomain.CurrentDomain.BaseDirectory + "hadconfig.json");
    }
}
