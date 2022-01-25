using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ntr_mysqlDatabase.Data
{
    public class Project
    {
        public int Id { get; set; }
        public string Code { set; get; }
        public User Manager { set; get; }
        public int Budget { set; get; }
        public bool Active { set; get; }
    }
}
