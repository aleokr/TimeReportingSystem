using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using ntr_mysqlDatabase.Data;
using ntr_mysqlDatabase.Models;
using ntr_mysqlDatabase.Repository;
using System;
using System.Collections.Generic;
namespace ntr_mysqlDatabase.Controllers
{
    public class ActivityController : Controller
    {
        private readonly ILogger<ActivityController> _logger;
        private ProjectRepository projectRepository;
        private ActivityRepository activityRepository;

        public ActivityController(ILogger<ActivityController> logger, MySqlDbContext mySqlDbContext)
        {
            _logger = logger;
            projectRepository = new ProjectRepository(mySqlDbContext);
            activityRepository = new ActivityRepository(mySqlDbContext);
        }
        public ActionResult Add()
        {
            List<ProjectData> projects = projectRepository.GetActiveProjects();
            AddActivityFormData activityFormData = new AddActivityFormData
            {
                Projects = projects,
                Date = DateTime.Now
            };
            return View(activityFormData);
        }

        [HttpPost]
        public ActionResult Create(AddActivityFormData activityFormData)
        {
            try
            {
                int userId = Int32.Parse(HttpContext.Session.GetString("userId"));
                if (!activityRepository.checkIfMonthIsActive(userId, activityFormData.Activity.Date))
                {
                    return RedirectToAction("Closed", new RouteValueDictionary(
                            new { controller = "Activity", action = "Closed" }));
                }
                activityRepository.addActivity(activityFormData, userId);
                return RedirectToAction("Day", new RouteValueDictionary(
                            new { controller = "Report", action = "Day", date = activityFormData.Activity.Date }));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Accept(string date, int time, string code, string username, int id)
        {
            AcceptActivityFormData acceptActivityFormData = new AcceptActivityFormData
            {
                Date = date,
                Time = time,
                Code = code,
                Username = username,
                ActivityId = id
            };
            return View(acceptActivityFormData);
        }

        [HttpPost]
        public ActionResult Submit(AcceptActivityFormData acceptActivityFormData)
        {
            try
            {
                activityRepository.submitActivity(acceptActivityFormData);
                return RedirectToAction("Projects", new RouteValueDictionary(
               new { controller = "Report", action = "Projects" }));
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int activityId, string type, string date)
        {
            activityRepository.deleteActivity(activityId);
            return RedirectToAction(type, new RouteValueDictionary(
                new { controller = "Report", action = type, date = date }));
        }

        public ActionResult Closed()
        {
            return View();
        }
        public ActionResult CloseMonth(string date)
        {
            activityRepository.CloseMonth(Int32.Parse(HttpContext.Session.GetString("userId")), DateTime.Parse(date).ToString("yyyy-MM-dd"));
            return RedirectToAction("Month", new RouteValueDictionary(
               new { controller = "Report", action = "Month", date = date }));
        }
    }
}
