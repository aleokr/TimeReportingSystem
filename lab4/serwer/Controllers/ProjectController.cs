using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using ntr_mysqlDatabase.Data;
using ntr_mysqlDatabase.Models;
using ntr_mysqlDatabase.Repository;

using System.Collections.Generic;
namespace ntr_mysqlDatabase.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private ProjectRepository projectRepository;
        private readonly MySqlDbContext _dbContext;

        public ProjectController(ILogger<ProjectController> logger, MySqlDbContext mySqlDbContext)
        {
            _logger = logger;
            _dbContext = mySqlDbContext;
            projectRepository = new ProjectRepository(mySqlDbContext);
        }

        [HttpPost]
        [Route("create")]
        public ActionResult<Project> Create(ProjectFormData projectFormData)
        {
            try
            {
                Project existingproject = projectRepository.checkIfProjectExists(projectFormData.Code);
                if (existingproject != null)
                {
                    Response.StatusCode = 403;
                    return null;
                }

                return projectRepository.createProject(projectFormData);
            }
            catch
            {
                Response.StatusCode = 500;
                return null;
            }
        }

        [HttpPut]
        [Route("close/{code}")]
        public ActionResult Close(string code)
        {
            try
            {
                projectRepository.closeProject(code);
                Response.StatusCode = 200;
            }
            catch
            {
                Response.StatusCode = 500;
            }
            return NoContent();
        }

        [HttpGet]
        [Route("subprojects/{code}")]
        public ActionResult<IEnumerable<Subproject>> GetSubprojects(string code)
        {
            try
            {
                return projectRepository.getSubprojects(code);

            }
            catch
            {
                Response.StatusCode = 500;
                return NoContent();
            }

        }

        [HttpGet]
        [Route("all")]
        public ActionResult<IEnumerable<Project>> GetProjects()
        {
            try
            {
                return projectRepository.getProjects();

            }
            catch
            {
                Response.StatusCode = 500;
                return NoContent();
            }

        }
    }
}
