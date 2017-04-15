using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWakeMeUp.Entity
{
    abstract public class ActivityItem
    {
        abstract public int ItemId { get; set; }
        abstract public int DayId { get; set; }
        abstract public int StepId { get; set; }
        abstract public Time Timespan { get; set; }
        abstract public int TimeOffset { get; set; }
    }
}
