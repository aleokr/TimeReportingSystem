using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ntr_mysqlDatabase.Models
{
    public class ProjectData
    {
        public string Code { set; get; }
        public string Manager { set; get; }
        public int Budget { set; get; }
        public bool Active { set; get; }
        public List<SubprojectData> Subprojects { set; get; }
    }
}
