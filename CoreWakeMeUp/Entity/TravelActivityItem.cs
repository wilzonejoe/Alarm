using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWakeMeUp.Entity
{
    public class TravelActivityItem : ActivityItem
    {
        public string Title { get; set; }
        public int TravelTime { get; set; }
        public string TransportCode { get; set; }
        public string SourceAddress { get; set; }
        public string DestinationAddress { get; set; }
    }
}
