using System.Data.Entity;
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
            Database.SetInitializer(new FacultyDbContextInitializer());
        }

        /// <summary>
        ///     Dbset for courses
        /// </summary>
        public DbSet<CourseEntity> Courses { get; set; }

        /// <summary>
        ///     Dbset for themes
        /// </summary>
        public DbSet<ThemeEntity> Themes { get; set; }

        /// <summary>
        ///     Dbset for marks
        /// </summary>
        public DbSet<MarkEntity> Marks { get; set; }
    }
}