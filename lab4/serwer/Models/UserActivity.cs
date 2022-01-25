using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ntr_mysqlDatabase.Models
{
    public class UserActivity
    {
        public string Name { set; get; }
        public int Id { set; get; }
        public int UserId { set; get; }
        public string Date { set; get; }
        public string Code { set; get; }
        public string Subcode { set; get; }
        public int Time { set; get; }
        public string Description { set; get; }
        public bool CanEdit { set; get; }
        public bool Accepted { set; get; }

    }
}
