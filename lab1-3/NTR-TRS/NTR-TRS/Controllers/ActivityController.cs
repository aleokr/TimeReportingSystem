using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NTR_TRS.Models;
using NTR_TRS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTR_TRS.Controllers
{
    public class ActivityController : Controller
    {
        private ActivityRepository _activityRepository = null;
        private ProjectRepository _projectRepository = null;
        private MonthRepository _monthRepository = null;

        public ActivityController()
        {
            _activityRepository = new ActivityRepository();
            _projectRepository = new ProjectRepository();
            _monthRepository = new MonthRepository();
        }
        public ActionResult Add()
        {
            var projects = _projectRepository.GetActiveProjects();
            AddActivityFormData activityFormData = new AddActivityFormData
            {
                Projects = projects,
                Date = DateTime.Now
            };
            return View(activityFormData);
        }

        // POST: ActivityController/Create
        [HttpPost]
        public ActionResult Create(AddActivityFormData activityFormData)
        {
            try
            {
                if(!_monthRepository.CheckIfMonthIsActive(HttpContext.Session.GetString("username"), activityFormData.Activity.Date))
                {
                    return RedirectToAction("Closed", new RouteValueDictionary(
                            new { controller = "Activity", action = "Closed"}));
                }
                UserActivity userActivity = new UserActivity
                {
                    Date = activityFormData.Activity.Date,
                    Time = activityFormData.Activity.Time,
                    Description = activityFormData.Activity.Description
                };
                if (activityFormData.Activity.Code.Contains(","))
                {
                    int commaLocation = activityFormData.Activity.Code.IndexOf(",", StringComparison.Ordinal);
                    int colonLocation = activityFormData.Activity.Code.IndexOf(":", StringComparison.Ordinal);
                    userActivity.Code = activityFormData.Activity.Code.Substring(0, commaLocation);
                    userActivity.Subcode = activityFormData.Activity.Code.Substring(colonLocation + 1);

                }
                else
                {
                    userActivity.Code = activityFormData.Activity.Code;
                }
                _activityRepository.AddActivity(userActivity, HttpContext.Session.GetString("username"));

                return RedirectToAction("Index", new RouteValueDictionary(
                            new { controller = "Home", action = "Index" }));
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Accept(string date, string username, int time, string code)
        {
            AcceptActivityFormData acceptActivityFormData = new AcceptActivityFormData
            {
                Date = date,
                Username = username,
                Time = time,
                Code = code
            };
            return View(acceptActivityFormData); 
        }

        [HttpPost]
        public ActionResult Submit(AcceptActivityFormData acceptActivityFormData)
        {
            try
            {
                UserActivity userActivity = new UserActivity
                {
                    Date = acceptActivityFormData.Date,
                    Time = acceptActivityFormData.Time,
                    Code = acceptActivityFormData.Code
                };
                _activityRepository.AcceptActivity(userActivity, acceptActivityFormData.Username);
                return RedirectToAction("Projects", new RouteValueDictionary(
               new { controller = "Home", action = "Projects"}));
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(String id, string type, string date)
        {
            _activityRepository.DeleteActivity(id, HttpContext.Session.GetString("username"));
            return RedirectToAction(type, new RouteValueDictionary(
                new { controller = "Home", action = type, date = date }));
        }

        public ActionResult Closed()
        {
            return View();
        }
        public ActionResult Submit(string date)
        {
            _monthRepository.CloseMonth(HttpContext.Session.GetString("username"), DateTime.Parse(date).ToString("yyyy-MM-dd"));
            return RedirectToAction("Month", new RouteValueDictionary(
               new { controller = "Home", action = "Month", date = date }));
        }
    }
}
