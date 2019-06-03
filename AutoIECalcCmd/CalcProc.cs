using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;
using AutoIECalcPublic;
using AutoFrameWork;

namespace AutoIECalcCmd
{
    interface ICalcProcess
    {
        string Do();
        void Dump();
    }

    abstract class CalcProc : ICalcProcess
    {
        public CalcProc()
        {
            OutputPath = "";
        }

        public string Do()
        {


            ClearProcess();
            CreateOutputPath();

            string BaseGPBPath = ConvertBaseStationDataToGPB();
            string RoverGPBPath = ConvertRoverGNSSDataToGDB();

            //string BaseGPBPath = @"D:\temp\@@2018-06-07-142430\BASE\Rinex\1025047158F2.gpb";
            //string RoverGPBPath = @"D:\temp\@@2018-06-07-142430\ROVER\062434_T.gpb";

            DifferentialGNSS(BaseGPBPath, RoverGPBPath);

            TightlyCoupleGNSSAndIMU();

            string output = ExportPostTFile();
            //ClearProcess();

            return output;

            //return "";
        }

        public void Dump()
        {
            processConvert = Application.TryFindProcess("wConvert", 3);
            if (processConvert != null)
            {
                Log.WARN(processConvert.GetWindowTree().ToString());
                //processConvert.ShowWindows();
            }

            processIE = Application.TryFindProcess("wGpsIns", 3);
            if (processIE != null)
            {
                Log.WARN(processIE.GetWindowTree().ToString());
                //processIE.ShowWindows();
            }

            processIE = Application.TryFindProcess("wConvertIMU", 3);
            if (processIE != null)
            {
                Log.WARN(processIE.GetWindowTree().ToString());
                //processIE.ShowWindows();
            }
        }

        public abstract string ConvertBaseStationDataToGPB();

        public abstract string ConvertRoverGNSSDataToGDB();

        public abstract void DifferentialGNSS(string basestationFle, string GNSSFile);

        public abstract void TightlyCoupleGNSSAndIMU();

        public abstract string ExportPostTFile();



        internal string CreateOutputPath()
        {
            Directory.CreateDirectory(config.GetProjectDir());
            return OutputPath;
        }

        internal void ClearProcess()
        {
            Application.Stop("wConvert");
            Application.Stop("wGpsIns");
            Application.Stop("wConvertIMU");
        }

        protected Application processConvert = null;
        protected Application processIE = null;

        protected static ConfigSetting config = null;// ConfigSetting.Load();
        protected string OutputPath;
    }
}