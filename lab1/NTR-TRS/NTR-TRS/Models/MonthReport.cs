using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NTR_TRS.Models
{
    public class MonthReport
    {
        public bool Frozen { set; get; }
        public List<UserActivity> Entries { set; get; }
        public List<UserActivity> Accepted { set; get; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
