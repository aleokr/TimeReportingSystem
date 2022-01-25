using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NTR_TRS.Models
{
    public class DayActivityData
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public List<UserActivity> Activities { get; set; }
    }
}
