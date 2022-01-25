using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTR_TRS.Models
{
    public class Project
    {
        public string Code { set; get; }
        public string Manager { set; get; }
        public int Budget { set; get; }
        public bool Active { set; get; }
        public List<Subproject> Subprojects { set; get; }
    }
}
