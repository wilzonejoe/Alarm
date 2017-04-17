using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWakeMeUp.Entity
{
    public abstract class ActivityItem : SaveAbleObject
    {
        public int DayId { get; set; }
        public int StepId { get; set; }
        public Time Timespan { get; set; }
        public int TimeOffset { get; set; }
    }
}
