using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoIECalcCmd
{
    class CalcFactory
    {
        internal static ICalcProcess Generate(string[] args)
        {
            if (args.Any(x=>x=="WEIYA"))
            {
                return new WeiyaCalcProcess();
            }

            else if (args.Any(x => x == "HAD"))
            {
                if(args.Any(x => x == "BASEFIN"))
                {
                    return new CalcProcHad(true);
                }
                else
                {
                    return new CalcProcHad(false);
                }
                
            }

            else if (args.Any(x => x == "BASE"))
            {
                return new CalcProcBaseLoc();
            }

            throw new NotImplementedException();
        }
    }
}
