using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWakeMeUp.Entity
{
    // id is different from saveableobject due to compatibility of the 
   public class User : SaveAbleObject
    { 
        public string Name { get; set; }
        public Guid externalID { get; set; }
        public bool LocalUser { get; set; }
    }
}



