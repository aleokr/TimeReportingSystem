namespace ntr_mysqlDatabase.Models
{
    public class ActivityForm
    {
        public string Date { set; get; }
        public string Code { set; get; }
        public string Subcode { set; get; }
        public string Description { set; get; }
        public int UserId { set; get; }
        public int Time { set; get; }
    }
}