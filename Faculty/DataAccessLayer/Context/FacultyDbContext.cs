using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Initializers;
using DataAccessLayer.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer.Context
{
    public class FacultyDbContext : IdentityDbContext<AppUser>
    {
        public FacultyDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Database.SetInitializer<FacultyDbContext>(new FacultyDbContextInitializer());
        }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<ThemeEntity> Themes { get; set; }
    }
}
