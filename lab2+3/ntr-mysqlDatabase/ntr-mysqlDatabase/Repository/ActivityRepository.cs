using Microsoft.EntityFrameworkCore;
using ntr_mysqlDatabase.Data;
using ntr_mysqlDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ntr_mysqlDatabase.Repository
{
    public class ActivityRepository
    {
        private readonly MySqlDbContext _dbContext;
        public ActivityRepository(MySqlDbContext mySqlDbContext)
        {
            _dbContext = mySqlDbContext;
        }

        public bool checkIfMonthIsActive(int userId, string date)
        {
            return _dbContext.Activities.Where(a => a.User.Id == userId && a.Date.Substring(0, 7) == date.Substring(0, 7) && a.Confirm == true).SingleOrDefault() != null ? false : true;
        }

        public void addActivity(AddActivityFormData activityFormData, int userId)
        {
            string code = null;
            string subcode = "";
            
            if (activityFormData.Activity.Code.Contains(","))
            {
                int commaLocation = activityFormData.Activity.Code.IndexOf(",", StringComparison.Ordinal);
                int colonLocation = activityFormData.Activity.Code.IndexOf(":", StringComparison.Ordinal);
                code = activityFormData.Activity.Code.Substring(0, commaLocation);
                subcode = activityFormData.Activity.Code.Substring(colonLocation + 1);
            }else
            {
                code = activityFormData.Activity.Code;
            }
            User user = _dbContext.Users.Where(u => u.Id == userId).Single();
            Project project = _dbContext.Projects.Where(p => p.Code == code).Single();

            Activity activity = new Activity
            {
                Date = activityFormData.Activity.Date,
                Code = code,
                Subcode = subcode,
                Time = activityFormData.Activity.Time,
                Description = activityFormData.Activity.Description,
                Confirm = false,
                Accepted = false,
                User = user,
                Project = project
            };

            _dbContext.Activities.Add(activity);
            _dbContext.SaveChanges(false);

        }

        public List<ProjectActivity> getUserProjectsActivity(int userId)
        {
            List<Project> projects = _dbContext.Projects.Where(p=> p.Manager.Id == userId).ToList();
            if (projects.Count == 0)
            {
                return null;
            }

            List<ProjectActivity> projectActivities = new List<ProjectActivity> { };
            foreach (Project project in projects)
            {
                List<Activity> activities = _dbContext.Activities.Where(a => a.Project.Manager.Id == userId && a.User != null).ToList();

                ProjectActivity projectActivity = new ProjectActivity {
                    Entries = new List<UserActivity> { }
                };
                int usedBudget = 0;

                foreach (Activity activity in activities)
                {
                    User user = _dbContext.Users.Where(u => u.Id == activity.UserId).SingleOrDefault();
                    UserActivity userActivity = new UserActivity
                    {
                        Date = activity.Date,
                        Code = activity.Code,
                        Subcode = activity.Subcode,
                        Description = activity.Description,
                        Time = activity.Time,
                        CanEdit = !activity.Confirm,
                        Id = activity.Id,
                        Accepted = activity.Accepted,
                        Name = user.Name + ' ' + user.Surname
                    };
                    usedBudget += activity.Time;
                   
                    projectActivity.Entries.Add(userActivity);

                }
          

                projectActivity.Budget = project.Budget - usedBudget;
                projectActivity.Active = project.Active;
                projectActivity.Code = project.Code;
                projectActivities.Add(projectActivity);
            }
            return projectActivities;
        }

        public MonthModelData getMonthReport(int userId, string date)
        {
            List<Activity> acceptedActivities = _dbContext.Activities.Where(a => a.User.Id == userId && a.Date.Substring(0, 7) == date.Substring(0, 7) && a.Accepted == true).ToList();
            List<Activity> nonAcceptedActivities = _dbContext.Activities.Where(a => a.User.Id == userId && a.Date.Substring(0, 7) == date.Substring(0, 7) && a.Accepted == false).ToList();


            bool frozen = true;
            foreach (Activity activity in nonAcceptedActivities)
            {
                if(activity.Confirm == false)
                {
                    frozen = false;
                    break;
                }
            }

            List<ProjectActivity> projectActivities = new List<ProjectActivity> { };
            foreach (Activity activity in nonAcceptedActivities)
            {
                UserActivity userActivity = new UserActivity
                {
                    Date = activity.Date,
                    Code = activity.Code,
                    Subcode = activity.Subcode,
                    Description = activity.Description,
                    Time = activity.Time,
                    CanEdit = !activity.Confirm,
                    Id = activity.Id,
                    UserId = userId
                    
                };
                int index = projectActivities.FindIndex(p => string.Equals(p.Code, activity.Code));
                if (index >= 0)
                {
                    projectActivities[index].Entries.Add(userActivity);
                }
                else
                {
                    ProjectActivity projectActivity = new ProjectActivity
                    {
                        Entries = new List<UserActivity> { },
                        Code = activity.Code
                    };
                    projectActivity.Entries.Add(userActivity);
                    projectActivities.Add(projectActivity);
                }
            }

            MonthModelData monthModelData = new MonthModelData
            {
                Date = DateTime.Parse(date),
                Frozen = frozen,
                Accepted = acceptedActivities,
                ProjectActivities = projectActivities
            };
            return monthModelData;
        }

        internal DayActivityData getActivityByDay(int userId, string date)
        {
            List<Activity> activities = _dbContext.Activities.Where(a => a.User.Id == userId && a.Date.Substring(0, 10) == date.Substring(0, 10)).ToList();

            DayActivityData day = new DayActivityData
            {
                Activities = activities,
                Date = DateTime.Parse(date)
            };
            return day;
        }

       
        public void submitActivity(AcceptActivityFormData acceptActivityFormData)
        {
            Activity activity = _dbContext.Activities.Where(a => a.Id == acceptActivityFormData.ActivityId).SingleOrDefault();
            if(activity != null)
            {
                activity.Time = acceptActivityFormData.Time;
                activity.Accepted = true;
                _dbContext.Activities.Update(activity);
                _dbContext.SaveChanges(false); 
               
            }
        }
        internal void deleteActivity(int activityId)
        {
            Activity activity = _dbContext.Activities.Where(a => a.Id == activityId).SingleOrDefault();
            if (activity != null)
            {
                _dbContext.Activities.Remove(activity);
                _dbContext.SaveChanges(false);
            }
        }

        internal void CloseMonth(int userId, string date)
        {
            List<Activity> activities = _dbContext.Activities.Where(a => a.User.Id == userId && a.Date.Substring(0, 7) == date.Substring(0, 7)).ToList();
            foreach (Activity activity in activities)
            {
                activity.Confirm = true;
                _dbContext.Activities.Update(activity);

            }
            _dbContext.SaveChanges(false);
        }
    }
}
