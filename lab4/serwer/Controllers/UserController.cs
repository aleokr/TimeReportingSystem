using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ntr_mysqlDatabase.Data;
using System.Collections.Generic;
using System.Linq;

namespace ntr_mysqlDatabase.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly MySqlDbContext _dbContext;

        public UserController(ILogger<UserController> logger, MySqlDbContext mySqlDbContext)
        {
            _logger = logger;
            _dbContext = mySqlDbContext;
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<IEnumerable<User>>  AllUsers()
        {
            return _dbContext.Users.ToList();
        }

    }
}
