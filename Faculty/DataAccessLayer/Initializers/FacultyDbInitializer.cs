using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccessLayer.Context;
using DataAccessLayer.Managers;
using DataAccessLayer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer.Initializers
{
    public class FacultyDbContextInitializer : DropCreateDatabaseAlways<FacultyDbContext>
    {
        /// <summary>
        ///     Seed for database context of faculty
        /// </summary>
        /// <param name="context">faculty database context</param>
        protected override void Seed(FacultyDbContext context)
        {
            ApplicationUserInit(context);
            ModelsInit(context);

            base.Seed(context);
        }

        /// <summary>
        ///     Initializer of users, roles etc.
        /// </summary>
        /// <param name="context">faculty database context</param>
        private void ApplicationUserInit(FacultyDbContext context)
        {
            var userManager = new AppUserManager(new UserStore<AppUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Creating 3 roles
            var roleAdmin = new IdentityRole {Name = "admin"};
            var roleTeacher = new IdentityRole {Name = "teacher"};
            var roleStudent = new IdentityRole {Name = "student"};
            // Creating role for user ban
            var roleBanned = new IdentityRole {Name = "banned"};

            // Adding roles to DB
            roleManager.Create(roleAdmin);
            roleManager.Create(roleTeacher);
            roleManager.Create(roleStudent);
            roleManager.Create(roleBanned);

            //Creating users
            var admin = new AppUser {Email = "admin@gmail.com", UserName = "admin@gmail.com"};
            var teacher = new AppUser {Email = "teacher@gmail.com", UserName = "teacher@gmail.com"};
            var student = new AppUser {Email = "student@gmail.com", UserName = "student@gmail.com"};
            var student1 = new AppUser {Email = "student1@gmail.com", UserName = "student1@gmail.com"};

            //Creating passwords for users
            var passwordAdmin = "Admin_222";
            var passwordTeacher = "Teacher_22";
            var passwordStudent = "Student_22";
            var passwordStudent1 = "Student_22";

            //Adding users to manager
            var resultAdmin = userManager.Create(admin, passwordAdmin);
            var resultTeacher = userManager.Create(teacher, passwordTeacher);
            var resultStudent = userManager.Create(student, passwordStudent);
            var resultStudent1 = userManager.Create(student1, passwordStudent1);

            // If creation is successful
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

        /// <summary>
        ///     Models initializer
        /// </summary>
        /// <param name="context">faculty database context</param>
        private void ModelsInit(FacultyDbContext context)
        {
            //Creating themes
            var themes = new List<ThemeEntity>
            {
                new ThemeEntity("Science"), new ThemeEntity("IT"), new ThemeEntity("Language")
            };

            //Creating courses
            var course = new CourseEntity(themes[0], "Physics", new DateTime(2020, 1, 12), new DateTime(2020, 3, 17));
            var course1 = new CourseEntity(themes[1], "Programming", new DateTime(2020, 4, 14),
                new DateTime(2020, 8, 5));
            course1.Teacher = context.Users
                .SingleOrDefault(u => u.Email == "teacher@gmail.com");
            //Adding users to course
            course.Teacher = context.Users
                .SingleOrDefault(u => u.Email == "teacher@gmail.com");
            course.Students.Add(context.Users.SingleOrDefault(u => u.Email == "student@gmail.com"));
            course.Students.Add(context.Users.SingleOrDefault(u => u.Email == "student1@gmail.com"));


            //Adding themes to context
            context.Themes.AddRange(themes);

            //Adding courses to context
            context.Courses.Add(course);
            context.Courses.Add(course1);

            //Adding courses to users
            context.Users.Include("courses").SingleOrDefault(u => u.Email == "teacher@gmail.com")
                .Courses
                .Add(course);
            context.Users?.Include("courses").SingleOrDefault(u => u.Email == "teacher@gmail.com")
                .Courses
                .Add(course1);
            context.Users.Include("courses").SingleOrDefault(u => u.Email == "student@gmail.com").Scourses
                .Add(course);
            context.Users.Include("courses").SingleOrDefault(u => u.Email == "student1@gmail.com").Scourses
                .Add(course);

            //Adding marks to context
            context.Marks.Add(new MarkEntity(course,
                context.Users.SingleOrDefault(u => u.Email == "student@gmail.com"), 12));

            //Saving changes to context
            context.SaveChanges();
        }
    }
}