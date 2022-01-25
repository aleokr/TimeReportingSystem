using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ntr_mysqlDatabase.Data;
using ntr_mysqlDatabase.Models;
using ntr_mysqlDatabase.Repository;
using System;
using System.Collections.Generic;

namespace ntr_mysqlDatabase.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportController : ControllerBase
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

        [HttpGet]
        [Route("day/{userId}/{date}")]
        public ActionResult<List<Activity>> Day(int userId, String date)
        {
            return activityRepository.getActivityByDay(userId, date.Substring(0,10)).Activities;
        }

        [HttpGet]
        [Route("month/{userId}/{date}")]
        public ActionResult<MonthModelData> Month(string date, int userId)
        {
            return activityRepository.getMonthReport(userId, date.Substring(0,10));
        }

        [HttpGet]
        [Route("projects/{userId}")]
        public ActionResult<IEnumerable<ProjectActivity>> Projects(int userId)
        {
            return activityRepository.getUserProjectsActivity(userId);
        }

    }
}
