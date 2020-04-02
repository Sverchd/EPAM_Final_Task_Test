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

        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<ThemeEntity> Themes { get; set; }
        public DbSet<MarkEntity> Marks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Course>().HasMany(c => c.students).WithMany(s => s.Courses).
        }
    }
}