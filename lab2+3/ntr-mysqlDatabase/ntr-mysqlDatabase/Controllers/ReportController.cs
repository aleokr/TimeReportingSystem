using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ntr_mysqlDatabase.Data;
using ntr_mysqlDatabase.Models;
using ntr_mysqlDatabase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ntr_mysqlDatabase.Controllers
{
    public class ReportController : Controller
    {
        private readonly ILogger<ReportController> _logger;
        private ProjectRepository projectRepository;
        private ActivityRepository activityRepository;

        public ReportController(ILogger<ReportController> logger, MySqlDbContext mySqlDbContext)
        {
            _logger = logger;
            projectRepository = new ProjectRepository(mySqlDbContext);
            activityRepository = new ActivityRepository(mySqlDbContext);
        }
        public IActionResult Day(int userId, String date)
        {
            if (userId != 0)
            {
                HttpContext.Session.SetString("userId", userId.ToString());

            }
            if (date == null)
            {
                date = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                date = DateTime.Parse(date).ToString("yyyy-MM-dd");
            }

            DayActivityData dayActivity = activityRepository.getActivityByDay(Int32.Parse(HttpContext.Session.GetString("userId")), date);
            return View(dayActivity);
        }

        public IActionResult Month(string date)
        {
            if (date == null)
            {
                date = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                date = DateTime.Parse(date).ToString("yyyy-MM-dd");
            }
            MonthModelData monthReport = activityRepository.getMonthReport(Int32.Parse(HttpContext.Session.GetString("userId")), date);
            return View(monthReport);
        }

        public ActionResult Projects()
        {
            List<ProjectActivity> projects = activityRepository.getUserProjectsActivity(Int32.Parse(HttpContext.Session.GetString("userId")));
            return View(projects);
        }

    }
}
