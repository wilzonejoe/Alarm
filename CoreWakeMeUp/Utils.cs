using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWakeMeUp
{
    public class Utils
    {
        public static long ToMilisecond(DateTime date)
        {
            DateTime jan1St1970 = new DateTime
                (1970, 1, 1, 0, 0, 0, date.Kind);
            return (long)(date - jan1St1970).TotalMilliseconds;
        }
    }
}
