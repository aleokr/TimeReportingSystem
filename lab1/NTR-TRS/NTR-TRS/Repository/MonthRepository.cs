using Newtonsoft.Json;
using NTR_TRS.Models;
using System;
using System.IO;

namespace NTR_TRS.Repository
{
    public class MonthRepository
    {
        private static string startFilesPath = "../NTR-TRS/Files/";
        public MonthReport GetMonthReport(string username, string date)
        {
            MonthReport monthReport =  getMonthByUsernameAndDate(username, date.Substring(0, 7));
            if(monthReport != null)
            {
                foreach (UserActivity activity in monthReport.Entries)
                {
                    activity.CanEdit = !monthReport.Frozen;
                }
                monthReport.Date = DateTime.Parse(date);
            }
            
            return monthReport;
            
        }
        public void CloseMonth(string username, string date)
        {
            MonthReport monthReport = getMonthByUsernameAndDate(username, date.Substring(0, 7));
            if (monthReport == null)
            {
                return;
            }

            monthReport.Frozen = true;
            string json = JsonConvert.SerializeObject(monthReport, Formatting.Indented);
            File.WriteAllText(startFilesPath + username + "-" + date.Substring(0, 7) + ".json", json);
        }
        public bool CheckIfMonthIsActive(string username, string date)
        {
            MonthReport monthReport = getMonthByUsernameAndDate(username, date.Substring(0, 7));
            if (monthReport == null)
            {
                return true;
            }
            return !monthReport.Frozen;
        }

        public MonthReport getMonthByFileName(string fileName)
        {
            try
            {
                using (StreamReader r = new StreamReader(fileName))
                {
                    string json = r.ReadToEnd();
                    MonthReport projects = JsonConvert.DeserializeObject<MonthReport>(json);
                    return projects;
                }
            }
            catch
            {
                return null;
            }
        }
        private MonthReport getMonthByUsernameAndDate(string username, string date)
        {
            try
            {
                using (StreamReader r = new StreamReader(startFilesPath + username + "-" + date + ".json"))
                {
                    string json = r.ReadToEnd();
                    MonthReport projects = JsonConvert.DeserializeObject<MonthReport>(json);
                    return projects;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
