using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ntr_mysqlDatabase.Models
{
    public class UpdateActivityFormData
    {        
        public string Description { set; get; }
        public int Time { set; get; }
        public bool Submit { set; get; }
        public int ActivityId { set; get; }
    }
}
