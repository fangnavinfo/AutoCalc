using Com.FirstSolver.Splash;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aimapCheck
{
    class CheckTrack2Photo
    {
        public CheckTrack2Photo(string path, string rlstpath, string currDate)
        {
            string syncPath = Directory.EnumerateFiles(path + @"\RawData\SYNC", "*.sync").First();

            syncStrings = ReadSyncFiles(syncPath, new string[] { "#TRG0", "$GPRMC" });

            this.rsltPath = rlstpath + "/Track2Photo/" + currDate + "/";
        }

        public void Process()
        {
            //int iStart = syncStrings.FindIndex(x => x.StartsWith("#TRG0"));
            //int iEnd = syncStrings.FindLastIndex(x => x.StartsWith("#TRG0"));

            //if (iStart == -1 || iEnd == -1)
            //{
            //    throw new Exception("can not find TRG0 in sync");
            //}

            //iStart = syncStrings.FindLastIndex(iStart, x => x.StartsWith("$GPRMC"));
            //iEnd = syncStrings.FindIndex(iEnd, x => x.StartsWith("$GPRMC"));

            //if (iStart == -1 || iEnd == -1)
            //{
            //    throw new Exception("can not find TRG0 in sync");
            //}

            Mapinfo.Mapdata mapdata = new Mapinfo.Mapdata();

            //bool HaveTRG0 = true;
            //string lastGPRMC = "";
            
            for (int i = 0; i < syncStrings.Count; i++)
            {
                try
                {
                    if (syncStrings[i].StartsWith("$GPRMC"))
                    {
                        bool HaveTRG0 = false;
                        var nextGPRMCIndex = syncStrings.FindIndex(i + 1, x => x.StartsWith("$GPRMC"));
                        var nextTRGIndex = syncStrings.FindIndex(i + 1, x => x.StartsWith("#TRG0"));
                        if (nextTRGIndex != -1 && nextGPRMCIndex > nextTRGIndex)
                        { 
                            HaveTRG0 = true;
                        }

                        var parse = GPRMC.Parse(syncStrings[i]);

                        double GCJ02_lat, GCJ02_lon;
                        MapConverter.WGS84ToGCJ02(parse.lat, parse.lon, out GCJ02_lat, out GCJ02_lon);

                        var point = mapdata.AddPoint(GCJ02_lat, GCJ02_lon, parse.speed, parse.direct, parse.datetime);
                        point.SetFlag(HaveTRG0);


                        //HaveTRG0 = false;
                        //lastGPRMC = syncStrings[i];

                        //if (HaveTRG0)
                        //{
                        //    var parse = GPRMC.Parse(syncStrings[i]);
                        //    var point = mapdata.AddPoint(parse.lat, parse.lon, parse.speed, parse.direct, parse.datetime);
                        //    point.SetFlag(true);


                        //    HaveTRG0 = false;
                        //    lastGPRMC = syncStrings[i];
                        //}
                        //else
                        //{
                        //    if (Distance(GPRMC.Loc(syncStrings[i]), GPRMC.Loc(lastGPRMC)) > 4.01)
                        //    {
                        //        var parse = GPRMC.Parse(syncStrings[i]);
                        //        var point = mapdata.AddPoint(parse.lat, parse.lon, parse.speed, parse.direct, parse.datetime);
                        //        point.SetFlag(false);
                        //    }

                        //    //var nextTRGIndex2 = syncStrings.FindIndex(nextGPRMCIndex + 1, x => x.StartsWith("#TRG0"));
                        //    //var nextGPRMCWithTRG = syncStrings.FindLastIndex(nextTRGIndex2, x => x.StartsWith("$GPRMC"));

                        //    //if (Distance(GPRMC.Loc(lastGPRMC), GPRMC.Loc(syncStrings[nextGPRMCWithTRG])) > 4.01)
                        //    //{
                        //    //    var parse = GPRMC.Parse(syncStrings[i]);
                        //    //    var point = mapdata.AddPoint(parse.lat, parse.lon, parse.speed, parse.direct, parse.datetime);
                        //    //    point.SetFlag(false);
                        //    //}
                        //}
                    }

                }
                catch (ArgumentException )
                {
                    continue;
                }
            }

            Directory.CreateDirectory(rsltPath);
            string path = rsltPath + "/check_report.tab";
            mapdata.WriteTab(path);
        }

        private double Distance((double lon, double lat) p1, (double lon, double lat) p2)
        {

            double radLat1 = Rad(p1.lat);
            double radLng1 = Rad(p1.lon);
            double radLat2 = Rad(p2.lat);
            double radLng2 = Rad(p2.lon);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * 6378137;
            return result;
        }

        private static double Rad(double d)
        {
            return (double)d * Math.PI / 180d;
        }

        private List<string> ReadSyncFiles(string path, string[] startWith)
        {
            List<string> rslt = new List<string>();
            StreamReader sr = new StreamReader(path);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                if (startWith.Any(x => line.StartsWith(x)))
                {
                    rslt.Add(line);
                }
            }

            return rslt;
        }

        private List<string> syncStrings;
        private string prjPath;
        private string rsltPath;
    }

    public class GPRMC
    {
        public static (double lon, double lat) Loc(string strGPRMC)
        {
            var parse = Parse(strGPRMC);
            return (parse.lat, parse.lon);
        }

        internal static (double lat, double lon, double speed, double direct, string datetime) Parse(string strGPRMC)
        {
            string[] split = strGPRMC.Split(',');

            string lat = split[3];
            string lon = split[5];
            string speed = split[7];
            string direct = split[8];
            string day = split[9];
            string time = split[1];

            day = string.Format("{0}{1}{2}", "20" + day.Substring(4, 2), day.Substring(2, 2), day.Substring(0, 2));

            if (lat == "" || lon == "")
            {
                throw new ArgumentException("lat or lon is empty: " + strGPRMC);
            }

            var Dlat = double.Parse(lat);
            Dlat = (int)Dlat / 100 + (Dlat - ((int)Dlat / 100) * 100) / 60;

            var DLon = double.Parse(lon);
            DLon = (int)DLon / 100 + (DLon - ((int)DLon / 100) * 100) / 60;

            return (Dlat, DLon, convetKnots2m_s(double.Parse(speed)), double.Parse(direct), day + "-" + time);
        }

        private static double convetKnots2m_s(double v)
        {
            return v * 0.514;
        }
    }
}
