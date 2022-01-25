using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ntr_mysqlDatabase.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ntr_mysqlDatabase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MySqlDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, MySqlDbContext mySqlDbContext)
        {
            _logger = logger;
            _dbContext = mySqlDbContext;
        }

        public IActionResult Index()
        {
            var data = _dbContext.Users.ToList();
            return View(data);
        }
       
    }
}
