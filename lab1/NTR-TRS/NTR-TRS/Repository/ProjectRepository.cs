using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NTR_TRS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NTR_TRS.Repository
{
    public class ProjectRepository
    {
        private static string projectsFilePath = "../NTR-TRS/Files/projects.json";
        private List<Project> projects = null;
        public List<Project> GetAllProjects()
        {
            projects = getAll();
            return projects;
        }

        public void AddProject (Project project)
        {
            projects = getAll();
            foreach (Project p in projects)
            {
                if (string.Equals(p.Code, project.Code))
                {
                    return;
                }
            }
            project.Active = true;
            projects.Add(project);
            string json = JsonConvert.SerializeObject(projects, Formatting.Indented);
            File.WriteAllText(projectsFilePath, json);
        }
        public Project GetProjectByCode(string code)
        {
            projects = getAll();
            foreach(Project project in projects)
            {
                if(string.Equals(project.Code, code))
                {
                    return project;
                }
            }
            return null;
        }
        public List<Project> GetProjectsByManager(string manager)
        {
            projects = getAll();
            List<Project> managerProjects = new List<Project> { };
            foreach (Project project in projects)
            {
                if (string.Equals(project.Manager, manager))
                {
                    managerProjects.Add(project);
                }
            }
            return managerProjects;
        }

        public List<Project> GetActiveProjects()
        {
            projects = getAll();
            List<Project> activeProjects = new List<Project> { };
            foreach (Project project in projects)
            {
                if (project.Active == true)
                {
                    activeProjects.Add(project);
                }
            }
            return activeProjects;
        }

        public void CloseProjectByCode(string code)
        {
            projects = getAll();
            foreach (Project project in projects)
            {
                if (string.Equals(project.Code, code))
                {
                    project.Active = false;
                }
            }
            string json = JsonConvert.SerializeObject(projects, Formatting.Indented);
            File.WriteAllText(projectsFilePath, json);
        }

        private List<Project> getAll()
        {
            using (StreamReader r = new StreamReader(projectsFilePath))
            {
                string json = r.ReadToEnd();
                List<Project> projects = JsonConvert.DeserializeObject<List<Project>>(json);
                return projects;
            }
        }
    }
}

