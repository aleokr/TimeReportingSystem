using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ntr_mysqlDatabase.Data
{
    public class Activity
    {
        public int Id { set; get; }
        public string Date { set; get; }
        public string Code { set; get; }
        public string Subcode { set; get; }
        public int Time { set; get; }
        public string Description { set; get; }
        public bool Confirm { set; get; }
        public bool Accepted { set; get; }
        public int UserId { set; get; }
        public Project Project { get; set; }
        public User User { get; set; }
    }
}
