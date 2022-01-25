using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTR_TRS.Models;
using NTR_TRS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTR_TRS.Controllers
{
    public class UserController : Controller
    {
        private UserRepository _usersRepository = null;

        public UserController()
        {
            _usersRepository = new UserRepository();
        }
        // GET: ProjectController
        public ActionResult Index()
        {
            var users = _usersRepository.GetAllUsers();
            return View(users);
        }
    }
}
