using ntr_mysqlDatabase.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ntr_mysqlDatabase.Models
{
    public class MonthModelData
    {
        public bool Frozen { set; get; }
        public List<ProjectActivity> ProjectActivities { set; get; }
        public List<Activity> Accepted { set; get; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
