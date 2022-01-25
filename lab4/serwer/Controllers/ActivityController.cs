using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using ntr_mysqlDatabase.Data;
using ntr_mysqlDatabase.Models;
using ntr_mysqlDatabase.Repository;
using System;
namespace ntr_mysqlDatabase.Controllers
{
    [Route("api/activities")]
    [ApiController]
    public class ActivityController : ControllerBase
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
        [HttpPost]
        [Route("create")]
        public ActionResult<Activity> Create(ActivityForm activityFormData)
        {
            try
            {
                int userId = activityFormData.UserId;
                if (!activityRepository.checkIfMonthIsActive(userId, activityFormData.Date.Substring(0,10)))
                {
                    Response.StatusCode = 403;
                    return null;
                }
                return activityRepository.addActivity(activityFormData, userId);

            }
            catch
            {
                Response.StatusCode = 500;
                    return null;
            }
        }

        [HttpPost]
        [Route("update")]
        public ActionResult Update(UpdateActivityFormData updateActivityFormData)
        {
            try
            {
                activityRepository.updateActivity(updateActivityFormData);
            }
            catch
            {
                Response.StatusCode = 500;
            }
            return NoContent();
        }

        [HttpDelete("{activityId}")]
        public ActionResult Delete(int activityId)
        {
            try
            {
                activityRepository.deleteActivity(activityId);
            }
            catch
            {
                Response.StatusCode = 500;
            }
            return null;
        }

        [HttpPut]
        [Route("closeMonth/{userId}/{date}")]
        public ActionResult CloseMonth(string date, int userId)
        {

            try
            {
                activityRepository.CloseMonth(userId, DateTime.Parse(date).ToString("yyyy-MM-dd"));
            }
            catch
            {
                Response.StatusCode = 500;
            }
            return NoContent();
        }

        [HttpGet("{activityId}")]
        public ActionResult<Activity> GetActivityById(int activityId)
        {
            try
            {
               return activityRepository.GetActivityById(activityId);
            }
            catch
            {
                Response.StatusCode = 500;
            }
            return null;
        }
    }
}
