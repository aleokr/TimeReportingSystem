using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NTR_TRS.Models
{
    public class AddActivityFormData
    {
        public List<Project> Projects { set; get; }
        public UserActivity Activity { set; get; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
