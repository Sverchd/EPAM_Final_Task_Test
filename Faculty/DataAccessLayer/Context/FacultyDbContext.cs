using System.Data.Entity;
using BusinessLogicLayer.Models;
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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //modelBuilder.Entity<Course>().HasMany(c => c.students).WithMany(s => s.Courses).

        }
    }

}
