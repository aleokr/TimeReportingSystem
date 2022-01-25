using ntr_mysqlDatabase.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ntr_mysqlDatabase.Models
{
    public class AddActivityFormData
    {
        public List<ProjectData> Projects { set; get; }
        public UserActivity Activity { set; get; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
