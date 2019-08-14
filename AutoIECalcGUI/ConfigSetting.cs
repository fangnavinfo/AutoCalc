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

        public static string errfile
        {
            get
            {
                return System.AppDomain.CurrentDomain.BaseDirectory + "error.txt";
            }
        }

        public string RawPath;
        public string IEPath;

        public string BaseDataPath;
        public string RoverDataPath;
        public string OutputPath;
        public string PreprocessPath;

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
        public string endTime;

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
            //startTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            string json = JsonMapper.ToJson(this);
            System.IO.File.WriteAllText(filePath, json, Encoding.UTF8);
        }

        public void Save(string path)
        {
            //startTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            string json = JsonMapper.ToJson(this);
            System.IO.File.WriteAllText(path, json, Encoding.UTF8);
        }

        internal string GetCalcBaseProjectPath()
        {
            return PreprocessPath + "CalcBase" + ".proj";
        }

        internal string GetCalcBaseOutputPath()
        {
            return PreprocessPath + "CalcBase" + ".txt";
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

        public string GetRawBaseStationFileName()
        {
            string name = (from x in Directory.EnumerateFiles(GetRawBaseStationDir(), "*.1?o")
                           select x).FirstOrDefault();

            if (name == null)
            {
                name = (from x in Directory.EnumerateFiles(GetRawBaseStationDir(), "*.LOG")
                        select x).FirstOrDefault();
            }

            if (name == null)
            {
                name = (from x in Directory.EnumerateFiles(GetRawBaseStationDir(), "*.DAT")
                        select x).FirstOrDefault();
            }

            if(name == null)
            {
                return "";
            }

            return name.Substring(name.LastIndexOf(@"\") + 1);
        }

        public string GetRawRoverGNSSDir()
        {
            return RoverDataPath;
        }

        public string GetIE860ExePath()
        {
            return IEPath + "\\wGpsIns.exe";
        }

        public string GetPostprocessPath()
        {
            return PreprocessPath.TrimEnd(@"\".ToCharArray()) + @"\" + outputName + ".POST";
        }

        public string GetProjectCfgPath()
        {
            return PreprocessPath.TrimEnd(@"\".ToCharArray()) + @"\" + outputName +".proj";
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

        public void Init(string rawRootPath)
        {
            RawPath = rawRootPath;

            RoverDataPath = rawRootPath + @"RawData\ROVER\";
            if (!Directory.Exists(RoverDataPath))
            {
                throw new Exception(string.Format("默认流动站路径{0} 不存在，无法解算！", RoverDataPath));
            }

            if (!Directory.EnumerateFiles(RoverDataPath, "*.TXT").Any() && !Directory.EnumerateFiles(RoverDataPath, "*.gps").Any())
            {
                throw new Exception("流动站目录无法找到 *.TXT/*.gps 文件 " + RoverDataPath);
            }

            OutputPath = rawRootPath + @"Output\";

            string offsetFilePath = OutputPath + "天线偏移参数.txt";
            if (!File.Exists(offsetFilePath))
            {
                throw new Exception(string.Format("默认偏移参数文件{0} 不存在，无法解算！", offsetFilePath));
            }
            else
            {
                string param = File.ReadAllLines(offsetFilePath, Encoding.GetEncoding("gbk")).First();
                if (!param.Contains("天线："))
                {
                    throw new Exception(string.Format("默认偏移参数文件 {0} 解析失败，无法解算！", offsetFilePath));
                }

                try
                {
                    var splits = param.Replace("天线：", "").Split(' ');
                    LeverArmOffsetX = double.Parse(splits[0]).ToString();
                    LeverArmOffsetY = double.Parse(splits[1]).ToString();
                    LeverArmOffsetZ = double.Parse(splits[2]).ToString();
                }
                catch (Exception)
                {

                    throw new Exception(string.Format("默认偏移参数文件 {0} 解析失败，无法解算！", offsetFilePath));
                }
            }

            PreprocessPath = rawRootPath + @"Preprocess\";
            outputName = rawRootPath.Substring(rawRootPath.IndexOf("@@")+2).Replace("\\", "");

            BaseDataPath = rawRootPath + @"RawData\BASE\";
            if (!Directory.Exists(BaseDataPath))
            {
                throw new ArgumentException(string.Format("默认基站路径{0} 不存在，无法解算！", BaseDataPath));
            }

            if (!Directory.EnumerateFiles(BaseDataPath, "*.1?o").Any() && !Directory.EnumerateFiles(BaseDataPath, "*.LOG").Any()
                && !Directory.EnumerateFiles(BaseDataPath, "*.DAT").Any() && !Directory.EnumerateFiles(BaseDataPath, "*.gpb").Any())
            {
                throw new ArgumentException("基站目录无法找到 *.1?o/*.LOG/*.DAT/*.gpb 文件 " + BaseDataPath);
            }
        }

        private string projectDir = null;
        private string projectPath = null;
        
        private string baseprojPath = null;
        private string filePath;

        
    }
}
