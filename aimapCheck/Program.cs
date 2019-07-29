using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace aimapCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Log.INFO("Process Start: " + string.Join(" ", args));

                if (args.Count() < 1)
                {
                    throw new ArgumentException("参数个数错误");
                }

                string rlstpath = args[0] + @"\Check\";
                var currDate = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                Report.Init(rlstpath);

                //DataIntegrityCheck dataCheck = new DataIntegrityCheck(args[0]);
                //dataCheck.Process();

                if (args.Any(x => x == "-check_track2photo"))
                {
                    CheckTrack2Photo check = new CheckTrack2Photo(args[0], rlstpath, currDate);
                    check.Process();
                }

                if (args.Any(x => x == "-check_basesatelite"))
                {
                    CheckBaseSatellite check = new CheckBaseSatellite(args[0], rlstpath, currDate);
                    check.Process();
                }

                if (args.Any(x => x == "-check_basetime"))
                {
                    CheckBaseTime check = new CheckBaseTime(args[0], rlstpath, currDate);
                    check.Process();
                }

                var exposureCheckIndex = Array.FindIndex(args, x => x == "-check_exposure");
                if (exposureCheckIndex != -1)
                {
                    int step = 1;
                    if (args.Count() > exposureCheckIndex + 1)
                    {
                        try
                        {
                            step = int.Parse(args[exposureCheckIndex + 1]);
                        }
                        catch
                        {

                        }
                    }

                    if (step < 1)
                    {
                        throw new ArgumentException();
                    }

                    ImageCheck imageCheck = new ImageCheck(args[0], step, rlstpath);
                    imageCheck.Process();
                }

                Log.INFO("Process Finish");
            }
            catch (Exception e)
            {
                Log.INFO("Process Failed!" + e.Message + "\n" + e.StackTrace);
            }
        }

    }
}
