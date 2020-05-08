using PersonalWeb.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PersonalWeb.Models.DataContext
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<ProjectsDone> ProjectsDone { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<ContactingClients> ContactingClients { get; set; }

        public DatabaseContext()
        {
            //Database.SetInitializer(new MyInitializer());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, PersonalWeb.Migrations.Configuration>());
        }
    }   
}