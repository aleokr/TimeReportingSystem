using ntr_mysqlDatabase.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ntr_mysqlDatabase.Models
{
    public class DayActivityData
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public List<Activity> Activities { get; set; }
    }
}
