using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using ntr_mysqlDatabase.Data;
using ntr_mysqlDatabase.Models;
using ntr_mysqlDatabase.Repository;
using System;

namespace ntr_mysqlDatabase.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ILogger<ProjectController> _logger;
        private ProjectRepository projectRepository;

        public ProjectController(ILogger<ProjectController> logger, MySqlDbContext mySqlDbContext)
        {
            _logger = logger;
            projectRepository = new ProjectRepository(mySqlDbContext);
        }
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProjectFormData projectFormData)
        {
            try
            {
                Project existingproject = projectRepository.checkIfProjectExists(projectFormData.Code);
                if (existingproject != null)
                {
                    return RedirectToAction("exist", new RouteValueDictionary(
                            new { controller = "project", action = "exist", name = existingproject.Code, manager = existingproject.Manager }));
                }

                projectRepository.createProject(projectFormData, Int32.Parse(HttpContext.Session.GetString("userId")));

                return RedirectToAction("Projects", new RouteValueDictionary(
                            new { controller = "Report", action = "Projects" }));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Exist(string name, User manager)
        {
            var project = new Project
            {
                Code = name,
                Manager = manager
            };
            return View(project);
        }

        public ActionResult Close(string code)
        {
            projectRepository.closeProject(code);
            return RedirectToAction("Projects", new RouteValueDictionary(
                            new { controller = "Report", action = "Projects" }));
        }
    }
}
