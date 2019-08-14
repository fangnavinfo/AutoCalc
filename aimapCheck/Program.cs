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
        static int Main(string[] args)
        {
            try
            {

                Log.INFO("Process Start: " + string.Join(" ", args));

                if (args.Count() < 2)
                {
                    throw new ArgumentException("参数个数错误");
                }

                if (!Directory.Exists(args[0]))
                {
                    throw new ArgumentException("文件目录不存在, " + args[0]);
                }

                string rlstpath = args[0] + @"\Check\";
                var currDate = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var query = args.FirstOrDefault(x => x.Contains("-dir--"));
                if (query != null)
                {
                    currDate = query.Replace("-dir--", "");
                }

                Report.Init(rlstpath);

                //DataIntegrityCheck dataCheck = new DataIntegrityCheck(args[0]);
                //dataCheck.Process();
                int rslt = -1;

                if (args[1] == "-check_track2photo")
                {
                    CheckTrack2Photo check = new CheckTrack2Photo(args[0], rlstpath, currDate);
                    rslt = check.Process();
                }
                else if (args[1] == "-check_basetime")
                {
                    CheckBaseTime check = new CheckBaseTime(args[0], rlstpath, currDate);
                    rslt = check.Process();
                }
                else if (args[1].StartsWith("-check_exposure"))
                {
                    int step = 1;
                    if(args[1].StartsWith("-check_exposure--step:"))
                    {
                        var str = args[1].Replace("-check_exposure--step:", "");
                        step = int.Parse(str);
                    }
                    ImageCheck imageCheck = new ImageCheck(args[0], step, rlstpath, currDate);
                    rslt = imageCheck.Process();
                }
                else
                {
                    throw new Exception("not support param:" + args[1]);
                }

                Log.INFO("Process Finish");
                return rslt;
            }
            catch (Exception e)
            {
                Log.INFO("Process Failed!" + e.Message + "\n" + e.StackTrace);
                return -1;
            }
        }

    }
}
