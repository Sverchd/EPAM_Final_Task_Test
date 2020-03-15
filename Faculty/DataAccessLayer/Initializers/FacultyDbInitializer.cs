using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Managers;
using DataAccessLayer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer.Initializers
{
    //CreateDatabaseIfNotExists<FacultyDbContext>
    
    public class FacultyDbContextInitializer : DropCreateDatabaseAlways<FacultyDbContext>
    {
        protected override void Seed(FacultyDbContext context)
        {
            ApplicationUserInit(context);
            ModelsInit(context);

            base.Seed(context);
        }

        private void ApplicationUserInit(FacultyDbContext context)
        {
        
          var userManager = new AppUserManager(new UserStore<AppUser>(context));
        
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        
           // Create 3 roles
            var roleAdmin = new IdentityRole { Name = "admin" };

            var roleTeacher = new IdentityRole { Name = "teacher" };
            
            var roleStudent = new IdentityRole { Name = "student" };
        
            // Add roles to DB
            roleManager.Create(roleAdmin);
            roleManager.Create(roleTeacher);
            roleManager.Create(roleStudent);
            var admin = new AppUser { Email = "admin@gmail.com", UserName = "admin@gmail.com" };
            var teacher = new AppUser { Email = "teacher@gmail.com", UserName = "teacher@gmail.com" };
            var student = new AppUser { Email = "student@gmail.com", UserName = "student@gmail.com" };
            var student1 = new AppUser { Email = "student1@gmail.com", UserName = "student1@gmail.com" };

            string passwordAdmin = "Admin_222";
            string passwordTeacher = "Teacher_22";
            string passwordStudent = "Student_22";
            string passwordStudent1 = "Student_22";

            var resultAdmin = userManager.Create(admin, passwordAdmin);
            var resultTeacher = userManager.Create(teacher, passwordTeacher);
            var resultStudent = userManager.Create(student, passwordStudent);
            var resultStudent1 = userManager.Create(student1, passwordStudent1);
            // If creation is succeeded
            if (resultAdmin.Succeeded
        
                && resultTeacher.Succeeded && resultStudent.Succeeded && resultStudent1.Succeeded)
            {
                // Add users to roles
                userManager.AddToRole(admin.Id, roleAdmin.Name);
        
                userManager.AddToRole(teacher.Id, roleTeacher.Name);

                userManager.AddToRole(student.Id, roleStudent.Name);

                userManager.AddToRole(student1.Id, roleStudent.Name);
            }
        }
        private void ModelsInit(FacultyDbContext context)
        {
            var themes = new List<ThemeEntity>()
            {
                new ThemeEntity("Science"),new  ThemeEntity("IT"), new ThemeEntity("Language")

            };

            var course = new CourseEntity(themes[0], "Physics", DateTime.Today, DateTime.Now);
            course.Teacher = context.Users.Where(u => u.Email == "teacher@gmail.com")
                .SingleOrDefault();
            course.students.Add(context.Users.Where(u => u.Email == "student@gmail.com").SingleOrDefault());
            course.students.Add(context.Users.Where(u => u.Email == "student1@gmail.com").SingleOrDefault());
            var course1 = new CourseEntity(themes[1], "Programming", new DateTime(2020,4,14), new DateTime(2020,8,5));
            context.Themes.AddRange(themes);
            context.Courses.Add(course);
            context.Courses.Add(course1);
            context.Users.Include("courses").Where(u=>u.Email== "teacher@gmail.com").SingleOrDefault().courses.Add(course1);
            context.SaveChanges();

            var s = context.Courses.ToList();

        }

    }
}
