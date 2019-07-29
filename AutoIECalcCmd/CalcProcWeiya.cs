using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using AutoIECalcPublic;
using AutoFrameWork;


namespace AutoIECalcCmd
{
    class WeiyaCalcProcess : CalcProc
    {
        public WeiyaCalcProcess()
        {
            config = ConfigSetting.Load(ConfigSetting.WeiyaConfigPath);
        }

        public override string ConvertBaseStationDataToGPB()
        {
            //return @"E:\Collect\WEIYA\@@1001-0002-190228-03\RawData\BASE\SHXZ0417.gpb";

            Log.INFO(string.Format("START convert base station data to gpb!"));

            var namse = Directory.EnumerateFiles(config.GetRawBaseStationDir());

            string name = (from x in Directory.EnumerateFiles(config.GetRawBaseStationDir(), "*.1?o")
                           select x).FirstOrDefault();
            if (name == null)
            {
                name = (from x in Directory.EnumerateFiles(config.GetRawBaseStationDir(), "*.LOG")
                        select x).FirstOrDefault();
            }

            if (name == null)
            {
                name = (from x in Directory.EnumerateFiles(config.GetRawBaseStationDir(), "*.DAT")
                        select x).FirstOrDefault();
            }

            ClearFile(config.GetRawBaseStationDir(), "*.gpb");

            Application app = Application.Launch(config.GetConvetGPBExePath());

            Window convertWin = app.FindWindow("Convert Raw GNSS data to GPB");
            convertWin.GetByIndex<Editor>(0).SetValue(config.GetRawBaseStationDir());
            //convertWin.Get<Button>("Add All").Click();

            name = name.Substring(name.LastIndexOf(@"\") + 1);
            convertWin.Get<ListBox>(name).Click();
            convertWin.Get<Button>("Add").Click();

            Window detectWin = app.FindWindow("Auto Detect");
            detectWin.Get<Button>("是(Y)").Click();
            detectWin.WaitExit();
            
            //convertWin.Get<ListItem>(rawBasePath).Click();
            //convertWin.Get<Button>("Options").Click();

            //Window rinexOptionWin = Window.Find("RINEX Options");
            //rinexOptionWin.Get<Button>("Static").Click();
            //rinexOptionWin.Get<Button>("OK").Click();

            convertWin.Get<Button>("Convert").Click();

            app.FindWindow(By.NameContains("Converting"));

            Window completeWin = app.FindWindow("Conversion Complete (1/1 files succeeded)", 30*60);
            convertWin.Get<Button>("Close").Click();

            app.Exit();

            string output = (from x in Directory.EnumerateFiles(config.GetRawBaseStationDir(), "*.gpb")
                             select x).First();

            Log.INFO(string.Format("SUCCESS convert base station data to gpb! output[{0}]", output));
            return output;
        }

        public override string ConvertRoverGNSSDataToGDB()
        {
            Log.INFO(string.Format("START convert rover gnss data to gpb!"));

            ClearFile(config.GetRawRoverGNSSDir(), "*.gpb");

            Application app = Application.Launch(config.GetConvetGPBExePath());

            Window convertWin = app.FindWindow("Convert Raw GNSS data to GPB");
            convertWin.GetByIndex<Editor>(0).SetValue(config.GetRawRoverGNSSDir());

            //string name = (from x in Directory.EnumerateFiles(config.GetRawRoverGNSSDir(), "*.imu")
            //               select x).First();
            //name = name.Substring(name.LastIndexOf(@"\") + 1);
            //convertWin.Get<ListBox>(name).Click();
            //convertWin.Get<Button>("Add").Click();

            //Window detectWin = app.FindWindow("Auto Detect");
            //detectWin.Get<Button>("是(Y)").Click();

            //name = (from x in Directory.EnumerateFiles(config.GetRawRoverGNSSDir(), "*.gps")
            //               select x).First();
            //name = name.Substring(name.LastIndexOf(@"\") + 1);
            //convertWin.Get<ListBox>(name).Click();
            //convertWin.Get<Button>("Add").Click();

            convertWin.Get<Button>("Auto Add All").Click();
            convertWin.Get<Button>("Convert").Click();

            app.FindWindow(By.NameContains("Converting NovAtel OEM/SPAN to GPB"));

            Window completeWin = app.FindWindow(By.NameContains("Conversion Complete"), 240);
            convertWin.Get<Button>("Close").Click();

            app.Exit();

            string output = (from x in Directory.EnumerateFiles(config.GetRawRoverGNSSDir(), "*.gpb")
                             select x).First();

            Log.INFO(string.Format("SUCCESS convert rover gnss data to gpb! output[{0}]", output));
            return output;
        }

        public override void DifferentialGNSS(string basestationFle, string GNSSFile)
        {
            Log.INFO(string.Format("START differential gnss"));

            processIE = Application.Launch(config.GetIE860ExePath());
            processIE.FindWindow("Version 8.80", 90).WaitExit();


            Window dowloadWin = processIE.TryFindWindow("Download Manufacturer Files");
            if (dowloadWin != null)
            {
                dowloadWin.Get<Button>("Close").Click();
            }

            Window mainWin = processIE.FindWindow("Waypoint - Inertial Explorer 8.80", 90);
            mainWin.GetByIndex<ToolbarButton>(1).Click();

            Window wizardWin = processIE.FindWindow("Project Wizard");
            while(true)
            {
                var button = wizardWin.Get<Button>("下一步(N) >");
                button.Click();
                Thread.Sleep(3000);

                try
                {
                    wizardWin.Get<Button>("Create");
                    break;
                }
                catch(Exception e)
                {

                }
            }


            wizardWin.Get<Button>("Create").Click();

            Window projectWin = processIE.FindWindow("Enter Project File");
            projectWin.GetByIndex<Editor>(0).SetValue(config.GetProjectCfgPath());
            projectWin.Get<Button>("保存(S)").Click();

            wizardWin.Get<Button>("下一步(N) >").Click();
            wizardWin.GetS<Button>("Browse")[0].Click();

            Window GNSSWin = processIE.FindWindow("Select GNSS File");
            GNSSWin.GetByIndex<Editor>(0).SetValue(GNSSFile);
            GNSSWin.GetByIndex<Editor>(0).GetValue();
            GNSSWin.Get<Button>("打开(O)").Click();

            Thread.Sleep(1000);

            string value = wizardWin.GetByIndex<Editor>(0).GetValue();
            if (value.Length == 0)
            {
                throw new Exception("GNSS File path is NULL");
            }

            wizardWin.Get<Button>("I have IMU data file in Waypoint (IMR) format").Click();
            wizardWin.GetS<Button>("Browse")[1].Click();

            Window IMUWin = processIE.FindWindow("Select IMU File (Waypoint Format)");
            IMUWin.GetByIndex<Editor>(0).SetValue(config.GetIMUFilePath());
            IMUWin.Get<Button>("打开(O)").Click();

            Thread.Sleep(1000);

            wizardWin.Get<Button>("下一步(N) >").Click();
            Window ConfirmWin = processIE.FindWindow(By.NameContains("Add"));
            ConfirmWin.Get<Button>("是(Y)").Click();

            wizardWin.Get<Button>("下一步(N) >").Click();
            wizardWin.Get<Button>("I would like to add base station data").Click();
            wizardWin.Get<Button>("Do not add precise files").Click();
            wizardWin.Get<Button>("下一步(N) >").Click();
            wizardWin.Get<ListBox>("Add Station from File").Click();
            wizardWin.Get<Button>("下一步(N) >").Click();

            Window childWin = processIE.FindWindow("Project Wizard");
            childWin.Get<Button>("Browse").Click();

            GNSSWin = processIE.FindWindow("Select GNSS File");
            GNSSWin.GetByIndex<Editor>(0).SetValue(basestationFle);
            
            GNSSWin.Get<Button>("打开(O)").Click();

            Thread.Sleep(3000);

            childWin.Get<Button>("下一步(N) >").Click();

            Thread.Sleep(5 * 1000);

            //Window errorWin1 = processIE.TryFindWindow("Error");
            //if (errorWin1 != null)
            //{
            //    errorWin1.Get<Button>("确定").Click();
            //    errorWin1.WaitExit();
            //}


            Action<Window> actionNomarl = (Window masterStationWin) =>
            {
                masterStationWin.GetByIndex<Editor>(2).SetValue(config.Lat[0]);
                masterStationWin.GetByIndex<Editor>(3).SetValue(config.Lat[1]);
                masterStationWin.GetByIndex<Editor>(4).SetValue(config.Lat[2]);

                masterStationWin.GetByIndex<Editor>(5).SetValue(config.Lon[0]);
                masterStationWin.GetByIndex<Editor>(6).SetValue(config.Lon[1]);
                masterStationWin.GetByIndex<Editor>(7).SetValue(config.Lon[2]);

                masterStationWin.GetByIndex<Editor>(8).SetValue(config.BasetStationHeight);
                masterStationWin.GetByIndex<Editor>(10).SetValue(config.AntennaMeasureHeight);

                masterStationWin.GetByIndex<ComboBox>(3).Select("WGS84");
                masterStationWin.GetByIndex<ComboBox>(4).Select("Generic");

                Thread.Sleep(1000);
            };

            Action<Window> actionException = (Window errorWin) =>
            {
                string desc = errorWin.GetByIndex<StaticText>(0).GetValue();
                if(!desc.Contains("No antenna with the name"))
                {
                    throw new Exception(desc);
                }

                Log.WARN("Record message:" + desc);

                errorWin.Get<Button>("确定").Click();
                Window masterStationWin = processIE.FindWindow("Master Station Position");
                actionNomarl(masterStationWin);
            };

            Thread.Sleep(3000);
            processIE.FindChildWindow("Master Station Position",
                                      actionNomarl,

                                      "Error",
                                      actionException);

            childWin.Get<Button>("下一步(N) >").Click();

            wizardWin.Get<ListBox>("Finish").Click();

            wizardWin.Get<Button>("下一步(N) >").Click();
            //wizardWin.Get<Button>("下一步(N) >").Click();
            wizardWin.Get<Button>("完成").Click();

            mainWin.GetMenu("Process", "Process GNSS\tF5").Click();
            Window processWin = processIE.FindWindow("Process GNSS");
            processWin.Get<Button>("Process").Click(0);

            processWin = processIE.FindWindow("Differential GNSS Pre-processing ...");
            processWin.WaitExit(5*60, ()=>
                                    {
                                        ListView listView = processWin.TryGet<ListView>(0);
                                        if (listView != null && listView.itemCount != 0)
                                        {
                                            string[] infos = listView.AllItem();
                                            foreach (string info in infos)
                                            {
                                                Log.WARN("Differential GNSS Preprocessing report:" + info);
                                            }

                                            processWin.Get<Button>("Continue").Click();
                                            processWin.WaitExit();
                                        }
                                    });

            Window processWin1 = processIE.FindWindow(By.NameContains("Processing Differential GNSS 1"));
            Window processWin2 = processIE.FindWindow(By.NameContains("Processing Differential GNSS 2"));
            processWin1.WaitExit(20*60);
            processWin2.WaitExit();

            Log.INFO(string.Format("SUCCESS differential gnss"));
            
        }

        public override void TightlyCoupleGNSSAndIMU()
        {
            Log.INFO(string.Format("START Tightly couple"));

            Application app = Application.FindProcess("wGpsIns");
            Window mainWin = app.FindWindow(By.NameContains("Inertial Explorer"));
            mainWin.GetMenu("Process", "Process TC (Tightly Coupled)").Click();

            Window tightWin = app.FindWindow("Process Tightly Coupled");
            //tightWin.Get<Button>("Advanced GNSS").Click();

            //Window GNSSetingDialog = Window.Find("TC GNSS Settings");
            //{
            //    GNSSetingDialog.GetByIndex<TabCtrl>(0).Select("Measurement");
            //    GNSSetingDialog.GetByIndex<Editor>(0).SetValue("2.00");
            //    GNSSetingDialog.Get<Button>("确定").Click();
            //}

            //tightWin.Get<Button>("Advanced IMU").Click();
            //Window IMUSetingDialog = tightWin.FindChildWindow("IMU Processing Settings");
            //{
            //    Thread.Sleep(1000);
            //    //IMUSetingDialog.GetByIndex<TabCtrl>(0).Select("Alignment");
            //    IMUSetingDialog.GetS<Button>("Options")[0].Click();

            //    Window AligmentDialog = Window.Find("Alignment Options");
            //    AligmentDialog.Get<Button>("Automated alignment").Click();

                

            //    AligmentDialog.Get<Button>("OK").Click();
            //    AligmentDialog.WaitExit();

            //    IMUSetingDialog.GetS<Button>("Options")[1].Click();
            //    AligmentDialog = Window.Find("Alignment Options");
            //    AligmentDialog.Get<Button>("Automated alignment").Click();
            //    AligmentDialog.Get<Button>("OK").Click();
            //    AligmentDialog.WaitExit();

            //    IMUSetingDialog.GetByIndex<TabCtrl>(0).Select("GNSS");
            //    IMUSetingDialog.GetByIndex<Editor>(0).SetValue("1.00");

            //    IMUSetingDialog.Get<Button>("确定").Click();
            //}

            tightWin.GetByIndex<Editor>(0).SetValue(config.LeverArmOffsetX);
            tightWin.GetByIndex<Editor>(1).SetValue(config.LeverArmOffsetY);
            tightWin.GetByIndex<Editor>(2).SetValue(config.LeverArmOffsetZ);

            tightWin.Get<Button>("Process").Click(0);
            Window processWin = app.FindWindow("Tightly Coupled Differential Pre-processing ...");
            processWin.WaitExit(5 * 60, () =>
                                        {
                                            ListView listView = processWin.TryGet<ListView>(0);
                                            if (listView != null && listView.itemCount != 0)
                                            {
                                                string[] infos = listView.AllItem();
                                                foreach (string info in infos)
                                                {
                                                    Log.WARN("Tightly Coupled Differential Preprocessing report:" + info);
                                                }

                                                processWin.Get<Button>("Continue").Click();
                                                processWin.WaitExit();
                                            }
                                        });

            Window childWin1 = app.FindWindow(By.NameContains("Processing GNSS-IMU TC 1"));
            Window childWin2 = app.FindWindow(By.NameContains("Processing GNSS-IMU TC 2"));
            childWin1.WaitExit(120*60);
            childWin2.WaitExit();

            Thread.Sleep(2000);

            mainWin.GetMenu("File", "Save Project").Click();
            Thread.Sleep(2000);

            Log.INFO(string.Format("SUCCESS Tightly couple"));
        }

        public override string ExportPostTFile()
        {
            Log.INFO(string.Format("START Export PostT File"));
            Application app = Application.FindProcess("wGpsIns");
            Window mainWin = app.FindWindow(By.NameContains("Inertial Explorer"));
            mainWin.GetMenu("Output", "Export Wizard").Click();

            Window exportWin = app.FindWindow("Export Coordinates Wizard");
            exportWin.Get<ListBox>("HUACE_Pos").Click();
            exportWin.Get<Button>("Epochs").Click();

            string output = config.GetPostprocessPath();
            exportWin.GetByIndex<Editor>(0).SetValue(output);
            Thread.Sleep(5000);

            exportWin = app.FindWindow("Export Coordinates Wizard");
            exportWin.Get<Button>("下一步(N) >").Click();
            Thread.Sleep(2000);

            exportWin.Get<Button>("下一步(N) >").Click();
            Thread.Sleep(2000);

            exportWin.Get<Button>("下一步(N) >").Click();
            Thread.Sleep(2000);

            exportWin.Get<Button>("下一步(N) >").Click();
            Thread.Sleep(2000);

            exportWin.Get<Button>("下一步(N) >").Click();
            Thread.Sleep(2000);

            exportWin.Get<Button>("完成").Click();

            //Window ProcessWin = app.FindWindow(output);
            //ProcessWin.WaitExit();

            //Thread.Sleep(3000);

            Log.INFO(string.Format("SUCESS Export PostT File"));

            mainWin.GetMenu("View", "Processing Summary").Click();

            Window summaryWin = app.FindWindow(By.NameContains("Processing Summary"));
            summaryWin.Get<Button>("Save").Click();

            Window saveWin = app.FindWindow(By.NameContains("Save Processing Summary"));
            saveWin.Get<Button>("是(Y)").Click();

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
    }
}
