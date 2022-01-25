using Newtonsoft.Json;
using NTR_TRS.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace NTR_TRS.Repository
{
    public class ActivityRepository
    {
        private static string startFilesPath = "../NTR-TRS/Files/";
        private MonthRepository monthRepository = null;
        private ProjectRepository projectRepository = null;

        public ActivityRepository()
        {
            monthRepository = new MonthRepository();
            projectRepository = new ProjectRepository();
        }
        public DayActivityData GetActivityByDay(string username, string date)
        {
            List<UserActivity> activities = new List<UserActivity> { };
            MonthReport monthReport = monthRepository.GetMonthReport(username, date.Substring(0,7));
            if(monthReport != null)
            {
                foreach (UserActivity userActivity in monthReport.Entries)
                {
                    if (string.Equals(userActivity.Date, date))
                    {
                        userActivity.CanEdit = !monthReport.Frozen;
                        activities.Add(userActivity);
                    }
                }
            }
            DayActivityData dayActivity = new DayActivityData
            {
                Activities = activities,
                Date = DateTime.Parse(date)
            };
            
            return dayActivity;
        }
       

        public void AddActivity(UserActivity userActivity, string username)
        {
            MonthReport monthReport = monthRepository.GetMonthReport(username, userActivity.Date.Substring(0, 7));
            if(monthReport == null)
            {
                monthReport = new MonthReport
                {
                    Frozen = false,
                    Entries = new List<UserActivity> { },
                    Accepted = new List<UserActivity> { }

                };
            }
            userActivity.Id = DateTime.Now.ToString();
            monthReport.Entries.Add(userActivity);
            string json = JsonConvert.SerializeObject(monthReport, Formatting.Indented);
            File.WriteAllText(startFilesPath + username + "-" + userActivity.Date.Substring(0, 7) + ".json", json);
        }

        public void EditActivity(UserActivity userActivity, string username)
        {
            MonthReport monthReport = monthRepository.GetMonthReport(username, userActivity.Date.Substring(0, 7));
            if (monthReport == null)
            {
                return;
            }
            foreach (UserActivity activity in monthReport.Entries)
            {
                if (string.Equals(userActivity.Id, activity.Id))
                {
                    activity.Time = userActivity.Time;
                    activity.Description = userActivity.Description;
                    activity.Subcode = userActivity.Subcode;
                }
            }

            string json = JsonConvert.SerializeObject(monthReport, Formatting.Indented);
            File.WriteAllText(startFilesPath + username + "-" + userActivity.Date.Substring(0, 7) + ".json", json);
        }

        public void AcceptActivity(UserActivity userActivity, string username)
        {
            MonthReport monthReport = monthRepository.GetMonthReport(username, userActivity.Date.Substring(0, 7));
            if (monthReport == null)
            {
                return;
            }
            foreach (UserActivity activity in monthReport.Entries)
            {
                if (string.Equals(userActivity.Code, activity.Code))
                {
                    activity.Accepted = true;
                    activity.Time = userActivity.Time;
                }
            }
            monthReport.Accepted.Add(userActivity);

            string json = JsonConvert.SerializeObject(monthReport, Formatting.Indented);
            File.WriteAllText(startFilesPath + username + "-" + userActivity.Date.Substring(0, 7) + ".json", json);
        }
        public void DeleteActivity(string id, string username)
        {
            MonthReport monthReport = monthRepository.GetMonthReport(username, id.Substring(0, 7));
            if (monthReport == null)
            {
                return;
            }

            List<UserActivity> activities = new List<UserActivity> { };
            foreach (UserActivity activity in monthReport.Entries)
            {
                if (!string.Equals(id, activity.Id))
                {
                    activities.Add(activity);
                }
            }
            monthReport.Entries = activities;
            string json = JsonConvert.SerializeObject(monthReport, Formatting.Indented);
            File.WriteAllText(startFilesPath + username + "-" + id.Substring(0, 7) + ".json", json);
        }

        public List<ProjectActivity> GetUserProjectsActivity(string username)
        {
            List<Project> projects = projectRepository.GetProjectsByManager(username);
            if(projects.Count == 0)
            {
                return null;
            }

            List<ProjectActivity> projectActivities = new List<ProjectActivity> { };
            foreach (Project project in projects)
            {
                ProjectActivity projectActivity = new ProjectActivity
                {
                    Code = project.Code,
                    ProjectUsers = new List<User> { }
                };
                int usedBudget = 0;

                string[] fileArray = Directory.GetFiles(startFilesPath);

                foreach(string fileName in fileArray)
                {
                    if(!string.Equals(fileName, "projects.json") && !string.Equals(fileName, "users.json"))
                    {
                        MonthReport monthReport = monthRepository.getMonthByFileName(fileName);

                        if(monthReport != null)
                        {
                            bool read = false;
                            string tmpFileName = fileName.Substring(17);
                            int charLocation = tmpFileName.IndexOf("-", StringComparison.Ordinal);
                            foreach (UserActivity userActivity in monthReport.Accepted)
                            {
                                if(string.Equals(userActivity.Code, project.Code))
                                {
                                    User user = new User
                                    {
                                        Name = tmpFileName.Substring(0, charLocation),
                                        Time = userActivity.Time,
                                        Date = userActivity.Date,
                                        CanAccepted = false,
                                        ActivityAccepted = true
                                    };
                                    projectActivity.ProjectUsers.Add(user);
                                    usedBudget += userActivity.Time;
                                    read = true;
                                    break;
                                }
                            }
                            if (!read)
                            {
                                User user = null;
                                foreach (UserActivity userActivity in monthReport.Entries)
                                {
                                    if (string.Equals(userActivity.Code, project.Code))
                                    {
                                        if (user == null)
                                        {
                                            user = new User
                                            {
                                                Name = tmpFileName.Substring(0, charLocation),
                                                Time = userActivity.Time,
                                                Date = tmpFileName.Substring(charLocation + 1, 7),
                                                CanAccepted = monthReport.Frozen,
                                                ActivityAccepted = false
                                            };
                                        }
                                        else
                                        {
                                            user.Time += userActivity.Time;
                                        }
                                       
                                        
                                    }
                                }
                                if(user != null)
                                {
                                    projectActivity.ProjectUsers.Add(user);
                                    usedBudget += user.Time;
                                }

                            }
                        }
                    }

                }


                projectActivity.Budget = project.Budget - usedBudget;
                projectActivity.Active = project.Active;
                projectActivities.Add(projectActivity);
            }
            return projectActivities;

        }

    }
}
