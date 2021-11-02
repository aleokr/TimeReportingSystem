using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTR_TRS.Models
{
    public class ProjectActivity
    {
        public string Code { set; get; }
        public List<User> ProjectUsers { set; get; }
        public double Budget { set; get; }

        public bool Active { set; get; }

        public List<UserActivity> Entries { set; get; }
    }
}
