using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ntr_mysqlDatabase.Models
{
    public class ActivityData
    {
        public List<ProjectData> Projects { set; get; }
        public UserActivity Activity { set; get; }
        public int UserId {get; set;}
    }
}
