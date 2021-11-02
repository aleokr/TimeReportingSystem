using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NTR_TRS.Models;
using NTR_TRS.Repository;
using System;
using System.Collections.Generic;
namespace NTR_TRS.Controllers
{
    public class ProjectController : Controller
    {
        private ProjectRepository _projectRepository = null;
        private HomeController homeController = null;

        public ProjectController()
        {
            _projectRepository = new ProjectRepository();
            homeController = new HomeController();
        }
        public ActionResult Add()
        {
            return View();
        }

        // POST: ProjectController/Create
        [HttpPost]
        public ActionResult Create(ProjectFormData projectFormData)
        {
            try
            {
                List<Project> projects = _projectRepository.GetAllProjects();
                Project existingProject = projects.FindLast(p => p.Code == projectFormData.Code);
                if (existingProject != null)
                {
                    return RedirectToAction("Exist", new RouteValueDictionary(
                            new { controller = "Project", action = "Exist", name = existingProject.Code , manager = existingProject.Manager}));
                }
                Project project = new Project
                {
                    Code = projectFormData.Code,
                    Budget = projectFormData.Budget,
                    Manager = HttpContext.Session.GetString("username"),
                    Active = true,
                    Subprojects = new List<Subproject> { }
                  
                };
                string[] subcodes = projectFormData.SubCodes.Split(',');
                foreach (String subcode in subcodes)
                {
                    Subproject subproject = new Subproject
                    {
                        Code = subcode
                    };
                    project.Subprojects.Add(subproject);

                }
                _projectRepository.AddProject(project);
                return RedirectToAction("Projects", new RouteValueDictionary(
                            new { controller = "Home", action = "Projects" }));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Exist(string name, string manager)
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
            _projectRepository.CloseProjectByCode(code);
            return RedirectToAction("Projects", new RouteValueDictionary(
                            new { controller = "Home", action = "Projects" }));
        }

    }
}
