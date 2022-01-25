using ntr_mysqlDatabase.Data;
using ntr_mysqlDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ntr_mysqlDatabase.Repository
{
    public class ProjectRepository
    {
        private readonly MySqlDbContext _dbContext;

        public ProjectRepository(MySqlDbContext mySqlDbContext)
        {
            _dbContext = mySqlDbContext;
        }
        public Project checkIfProjectExists(string code)
        {
            return _dbContext.Projects.Where(p => p.Code == code).SingleOrDefault();
        }
       
        public void createProject(ProjectFormData projectFormData, int userId)
        {
            User userDb = _dbContext.Users.Where(u => u.Id == userId).Single();
            Project project = new Project
            {
                Code = projectFormData.Code,
                Budget = projectFormData.Budget,
                Manager = userDb,
                Active = true

            };


            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
            if(projectFormData.SubCodes != null)
            {
                string[] subcodes = projectFormData.SubCodes.Split(',');
                foreach (String subcode in subcodes)
                {
                    Subproject subproject = new Subproject
                    {
                        Code = subcode,
                        Project = project

                    };
                    _dbContext.Subprojects.Add(subproject);

                }
                _dbContext.SaveChanges(false);
            }

        }

        public List<ProjectData> GetActiveProjects()
        {
            List<ProjectData> response = new List<ProjectData> { };

            List<Project> projects = _dbContext.Projects.Where(p => p.Active == true).ToList();
            foreach (Project project in projects)
            {
                List<Subproject> subprojects = _dbContext.Subprojects.Where(p => p.Project.Id == project.Id).ToList();
                List<SubprojectData> subprojectData = new List<SubprojectData> { };

                foreach (Subproject subproject in subprojects)
                {
                    SubprojectData sub = new SubprojectData
                    {
                        Code = subproject.Code

                    };
                    subprojectData.Add(sub);

                }
                ProjectData projectData = new ProjectData
                {
                    Subprojects = subprojectData,
                    Code = project.Code

                };
                response.Add(projectData);

            }
            return response;
        }
       
        public void closeProject(string code)
        {
            Project project = _dbContext.Projects.Where(p => p.Code == code).SingleOrDefault();
            if(project != null)
            {
                project.Active = false;
                _dbContext.Projects.Update(project);
                _dbContext.SaveChanges(false);
            }
        }
    }
}
