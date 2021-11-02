 using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NTR_TRS.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using NTR_TRS.Repository;
using System;
using System.Collections.Generic;

namespace NTR_TRS.Controllers
{
    public class HomeController : Controller
    {
        private ActivityRepository _activityRepository = null;
        private MonthRepository _monthRepository = null;

        public HomeController()
        {
            _activityRepository = new ActivityRepository();
            _monthRepository = new MonthRepository();
        }

        public ActionResult Index(string username, string date)
        {
            if(username != null)
            {
                HttpContext.Session.SetString("username", username);

            }
            if (date == null)
            {
                date = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                date = DateTime.Parse(date).ToString("yyyy-MM-dd");
            }
            var dayActivity = _activityRepository.GetActivityByDay(HttpContext.Session.GetString("username"), date); 
            return View(dayActivity);
        }

        public ActionResult Month(string date)
        {
            if(date == null)
            {
                date = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                date = DateTime.Parse(date).ToString("yyyy-MM-dd");
            }
            MonthReport monthReport = _monthRepository.GetMonthReport(HttpContext.Session.GetString("username"), date);
            MonthModelData monthModelData = new MonthModelData
            {
                Date = monthReport.Date,
                Frozen = monthReport.Frozen,
                Accepted = monthReport.Accepted
            };
            List<ProjectActivity> projectActivities = new List<ProjectActivity> { };
            foreach(UserActivity activity in monthReport.Entries)
            {
                int index = projectActivities.FindIndex(p => string.Equals(p.Code,activity.Code));
                if (index >= 0)
                {
                    projectActivities[index].Entries.Add(activity);
                }
                else
                {
                    ProjectActivity projectActivity = new ProjectActivity
                    {
                        Entries = new List<UserActivity> { },
                        Code = activity.Code
                    };
                    projectActivity.Entries.Add(activity);
                    projectActivities.Add(projectActivity);
                }
            }
            monthModelData.ProjectActivities = projectActivities;
            return View(monthModelData);
        }
        public ActionResult Projects()
        {
            var projects = _activityRepository.GetUserProjectsActivity(HttpContext.Session.GetString("username"));
            return View(projects);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
