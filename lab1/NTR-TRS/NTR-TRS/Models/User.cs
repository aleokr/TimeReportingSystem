using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTR_TRS.Models
{
    public class User
    {
        public string Name { set; get; }
        public int Time { set; get; }
        public string Date { set; get; }
        public bool CanAccepted { set; get; }
        public bool ActivityAccepted { set; get; }
    }
}
