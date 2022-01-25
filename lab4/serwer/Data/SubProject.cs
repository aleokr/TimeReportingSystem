using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ntr_mysqlDatabase.Data
{
    public class Subproject
    {
        public int Id { set; get; }
        public string Code { set; get; }
        public Project Project { set; get; }
    }
}
