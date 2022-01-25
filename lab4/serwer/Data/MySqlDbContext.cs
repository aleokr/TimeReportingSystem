using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ntr_mysqlDatabase.Data
{
    public class MySqlDbContext : DbContext
    {
        public MySqlDbContext (DbContextOptions<MySqlDbContext> options) : base(options)
        {
            //this.Configuration.LazyLoadingEnabled = false;

        }

        public DbSet<Activity> Activities { set; get; }
        public DbSet<User> Users { set; get; }
        
        public DbSet<Project> Projects { set; get; }
        public DbSet<Subproject> Subprojects { set; get; }
    }
}
