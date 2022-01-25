using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ntr_mysqlDatabase.Models
{
    public class AcceptActivityFormData
    {
        public string Code { set; get; }
        public int UserId { set; get; }

        public string Username { set; get; }
        public int Time { set; get; }
        public string Date { set; get; }
        public int ActivityId { set; get; }
    }
}
