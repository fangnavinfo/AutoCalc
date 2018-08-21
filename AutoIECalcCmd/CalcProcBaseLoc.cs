using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using AutoFrameWork;
using AutoIECalcPublic;

namespace AutoIECalcCmd
{
    internal class CalcProcBaseLoc : ICalcProcess
    {
        public string Do()
        {
            ClearProcess();
            CreateOutputPath();

            string BaseGPBPath = ConvertBaseStationDataToGPB();
            //string BaseGPBPath = @"D:\temp\@@2018-06-07-142430\BASE\Rinex\1025047158F2.gpb";
            AddRemoteFile(BaseGPBPath);
            AddPreciseFile();
            ProcessGNSS();

            string output = ExportPostTFile();
            ClearProcess();
            Process p = Process.Start("notepad", output);
            return output;
        }

        public void Dump()
        {
            var processDownload = Application.FindProcess("Download");
            if (processDownload != null)
            {
                Log.WARN(processDownload.GetWindowTree().ToString());
            }

            var processConvert = Application.FindProcess("wConvert");
            if (processConvert != null)
            {
                Log.WARN(processConvert.GetWindowTree().ToString());
            }

            var processIE = Application.FindProcess("wGpsIns");
            if (processIE != null)
            {
                Log.WARN(processIE.GetWindowTree().ToString());
            }
        }

        private void ClearProcess()
        {
            Application.Stop("Download");
            Application.Stop("wConvert");
            Application.Stop("wGpsIns");
            
        }

        internal void CreateOutputPath()
        {
            string Path = config.GetCalcBaseProjectPath();
            Path = Path.Substring(0, Path.LastIndexOf(@"\"));
            Directory.CreateDirectory(Path);
        }

        private string ConvertBaseStationDataToGPB()
        {
            Log.INFO(string.Format("START convert base station data to gpb!"));

            ClearFile(config.GetRawBaseStationDir(), "*.gpb");

            Application app = Application.Launch(config.GetConvetGPBExePath());

            Window convertWin = app.FindWindow("Convert Raw GNSS data to GPB");
            convertWin.GetByIndex<Editor>(0).SetValue(config.GetRawBaseStationDir());
            convertWin.Get<Button>("Add All").Click();

            string rawBasePath = (from x in Directory.EnumerateFiles(config.GetRawBaseStationDir(), "*.18o")
                                  select x).First();

            convertWin.Get<ListItem>(rawBasePath).Click();
            convertWin.Get<Button>("Options").Click();

            Window rinexOptionWin = app.FindWindow("RINEX Options");
            {
                rinexOptionWin.Get<Button>("Static").Click();
                rinexOptionWin.Get<Button>("OK").Click();
                rinexOptionWin.WaitExit();
            }

            convertWin.Get<Button>("Convert").Click();

            app.FindWindow("Converting RINEX to GPB (1/1)");

            Window completeWin = app.FindWindow("Conversion Complete (1/1 files succeeded)");
            convertWin.Get<Button>("Close").Click();

            app.Exit();

            string output = (from x in Directory.EnumerateFiles(config.GetRawBaseStationDir(), "*.gpb")
                             select x).First();

            Log.INFO(string.Format("SUCCESS convert base station data to gpb! output[{0}]", output));
            return output;
        }

        private void AddRemoteFile(string baseGPBPath)
        {
            Application processIE = Application.Launch(config.GetIE860ExePath());
            processIE.FindWindow("Version 8.60").WaitExit();


            Window dowloadWin = processIE.TryFindWindow("Download Manufacturer Files");
            if (dowloadWin != null)
            {
                dowloadWin.Get<Button>("Close").Click();
            }

            Window mainWin = processIE.FindWindow("Waypoint - Inertial Explorer 8.60");
            mainWin.GetByIndex<ToolbarButton>(0).Click();

            Thread.Sleep(2000);

            Window projWin = processIE.FindWindow("Select New Project Name");
            {
                projWin.GetByIndex<Editor>(0).SetValue(config.GetCalcBaseProjectPath());
                Thread.Sleep(2000);
                projWin.Get<Button>("保存(S)").Click();
                projWin.WaitExit();
            }

            mainWin.GetMenu("File", "Add Remote File").Click();
            Thread.Sleep(2000);
            Window remoteWin = processIE.FindWindow("Select Remote GNSS Data File");
            {
                remoteWin.GetByIndex<Editor>(0).SetValue(baseGPBPath);
                remoteWin.Get<Button>("打开(O)").Click();
                remoteWin.WaitExit();
            }

            remoteWin = processIE.FindWindow("Select Remote GNSS Data File");
            {
                remoteWin.GetByIndex<ComboBox>(0).Select(config.AntennaProfile);
                remoteWin.GetByIndex<Editor>(2).SetValue(config.AntennaMeasureHeight);

                //remoteWin.Get<Button>("Compute From Slant").Click();
                //Window computeWin = processIE.FindWindow("Compute From Slant Height");
                //{
                //    computeWin.GetByIndex<Editor>(0).SetValue(config.SlantMeasure);
                //    computeWin.GetByIndex<Editor>(1).SetValue(config.RadiusGround);
                //    computeWin.GetByIndex<Editor>(2).SetValue(config.OffsetARP2Ground);
                //    computeWin.Get<Button>("OK").Click();
                //    computeWin.WaitExit();
                //}

                remoteWin.Get<Button>("确定").Click();
                remoteWin.WaitExit();
            }

 
        }

        private void AddPreciseFile()
        {
            Application app = Application.FindProcess("wGpsIns");
            Window mainWin = app.FindWindow(By.NameContains("Inertial Explorer"));
            mainWin.GetMenu("File", "Add Precise Files").Click();
            Window preciseWin = app.FindWindow("Precise Files");
            {
                preciseWin.Get<Button>("Download").Click();

                Application downloadApp = Application.FindProcess("Download");
                Window processingWin = downloadApp.FindWindow("Processing ...");
                {
                    processingWin.WaitExit(10*60);
                }

                Thread.Sleep(5000);
                preciseWin.Get<Button>("OK").Click();
            }
        }

        private void ProcessGNSS()
        {
            Application app = Application.FindProcess("wGpsIns");
            Window mainWin = app.FindWindow(By.NameContains("Inertial Explorer"));
            mainWin.GetByIndex<ToolbarButton>(11).Click();
            Window processWin = app.FindWindow("Process GNSS");
            processWin.Get<Button>("Process").Click();

            Action<Window> pppAction = delegate (Window win)
                                       {
                                           Action<Button> continueAction = delegate (Button btn)
                                           {
                                               btn.Click();
                                           };
                                           win.WaitExit("Continue", continueAction);

                                           app.FindWindow(By.NameContains("Processing Precise Point Positioning")).WaitExit();
                                       };

            Action<Window> procAction = delegate (Window win)
                                        {
                                            win.WaitExit();
                                        };

            mainWin.WaitChildWindowThen("PPP Preprocessing ...", pppAction,
                                        "Processing Precise Point Positioning", procAction);

            Log.INFO(string.Format("SUCCESS differential gnss"));

        }

        private string ExportPostTFile()
        {
            Log.INFO(string.Format("START Export PostT File"));
            Application app = Application.FindProcess("wGpsIns");
            Window mainWin = app.FindWindow(By.NameContains("Inertial Explorer"));
            mainWin.GetByIndex<ToolbarButton>(20).Click();

            Window exportWin = app.FindWindow("Export Coordinates Wizard");
            exportWin.Get<ListBox>("P_BaseStationPPP").Click();

            exportWin.GetByIndex<Editor>(0).SetValue(config.GetCalcBaseOutputPath());
            string output = exportWin.GetByIndex<Editor>(0).GetValue();

            exportWin.Get<Button>("下一步(N) >").Click();
            exportWin.Get<Button>("下一步(N) >").Click();
            exportWin.Get<Button>("下一步(N) >").Click();
            exportWin.Get<Button>("完成").Click();

            Thread.Sleep(5000);

            Log.INFO(string.Format("SUCESS Export PostT File"));
            return output;
        }

        private void ClearFile(string strDir, string pattern)
        {
            string[] filenames = (from x in Directory.EnumerateFiles(strDir, pattern)
                                  select x).ToArray();
            foreach (var filename in filenames)
            {
                File.Delete(filename);
            }

            Log.INFO(string.Format("Clear old file:[{0}]", string.Join(", ", filenames)));
        }

        protected static ConfigSetting config = ConfigSetting.Load(ConfigSetting.BaseConfigPath);
    }
}