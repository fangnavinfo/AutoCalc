using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
using System.IO;

namespace AutoIECalcPublic
{
    class ConfigSetting
    {
        public static string WeiyaConfigPath
        {
            get
            {
                return System.AppDomain.CurrentDomain.BaseDirectory + "config_weiya.json";
            }
        }

        public static string HadConfigPath
        {
            get
            {
                return System.AppDomain.CurrentDomain.BaseDirectory + "config_had.json";
            }
        }

        public static string BaseConfigPath
        {
            get
            {
                return System.AppDomain.CurrentDomain.BaseDirectory + "config_base.json";
            }
        }

        public string IEPath;

        public string BaseDataPath;
        public string RoverDataPath;
        public string OutputPath;

        public string[] Lat = new string[3];
        public string[] Lon = new string[3];

        public string BasetStationHeight;
        public string AntennaMeasureHeight;
        public string AntennaProfile;

        public string SlantMeasure;
        public string RadiusGround;
        public string OffsetARP2Ground;

        public string LeverArmOffsetX ;
        public string LeverArmOffsetY ;
        public string LeverArmOffsetZ ;

        public string startTime;

        public string outputName;

        public static ConfigSetting Load(string path)
        {
            ConfigSetting configSetting = null;

            if (File.Exists(path))
            {
                string json = System.IO.File.ReadAllText(path, Encoding.UTF8);
                configSetting = JsonMapper.ToObject<ConfigSetting>(json);
            }
            else
            {
                configSetting = new ConfigSetting();
            }

            configSetting.filePath = path;

            return configSetting;
        }

        public void Save()
        {
            startTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            string json = JsonMapper.ToJson(this);
            System.IO.File.WriteAllText(filePath, json, Encoding.UTF8);
        }

        public void Save(string path)
        {
            startTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            string json = JsonMapper.ToJson(this);
            System.IO.File.WriteAllText(path, json, Encoding.UTF8);
        }

        internal string GetCalcBaseProjectPath()
        {
            return GetProjectDir() + @"\CalcBase" + ".cfg";
        }

        internal string GetCalcBaseOutputPath()
        {
            return GetProjectDir() + @"\CalcBase" + ".txt";
        }

        public string GetConvetGPBExePath()
        {
            return IEPath + "wConvert.exe";
        }

        public string GetConvetIMUExePath()
        {
            return IEPath + "wConvertIMU.exe";
        }

        public string GetRawBaseStationDir()
        {
            return BaseDataPath;
        }

        public string GetRawRoverGNSSDir()
        {
            return RoverDataPath;
        }

        public string GetIE860ExePath()
        {
            return IEPath + "wGpsIns.exe";
        }

        public string GetPostprocessPath()
        {
            return GetProjectDir() + @"\" + outputName + ".postT";
        }

        public string GetProjectCfgPath()
        {
            return GetProjectDir() + @"\" + outputName +".cfg";
        }

        public string GetProjectDir()
        {
            if (projectDir == null)
            {
                projectDir = OutputPath;
            }

            return projectDir;
        }

        public string GetIMUFilePath()
        {
            string path = (from x in Directory.EnumerateFiles(RoverDataPath, "*.imr")
                             select x).First();
            return path;
        }

        private string projectDir = null;
        private string projectPath = null;
        
        private string baseprojPath = null;
        private string filePath;

        
    }
}
