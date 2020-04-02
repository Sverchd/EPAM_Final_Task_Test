using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Models;
using DataAccessLayer.Context;
using DataAccessLayer.Managers;
using DataAccessLayer.Mappers;
using DataAccessLayer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FacultyDbContext _facultyDbContext;

        public UserRepository(FacultyDbContext facultyDbContext)
        {
            _facultyDbContext = facultyDbContext;
        }

        public List<User> GetAllTeachers()
        {
            var role = _facultyDbContext.Roles.SingleOrDefault(m => m.Name == "Teacher");
            var EnityTeachers = role.Users;

            var userIds = role.Users
                .Select(y => y.UserId)
                .ToList();

            //EnityTeachers =EnityTeachers.ToList();
            var entityTeachers = _facultyDbContext.Users
                .Include(us => us.Courses)
                .Where(us => userIds.Contains(us.Id))
                .ToList();

            //var teachers = new List<User>();

            //foreach (var eteacher in EnityTeachers)
            //{
            //    var entityTeacher = _facultyDbContext.Users.Include(x => x.courses.Select(y => y.Theme)).SingleOrDefault(u => u.Id == eteacher.UserId);
            //    //var courses = new List<Course>();
            //    var courses = entityTeacher.courses.Select(x => x.Map()).ToList();
            //    //TODO: Use mappers

            //    teachers.Add(new User(entityTeacher.Email, entityTeacher.Roles.ToString(), courses));
            //}

            //var d = "";
            //Получается циклический мап

            var resultTeachers = entityTeachers.AsEnumerable().Select(x => x.Map()).ToList();

            return resultTeachers;
        }

        public List<User> GetAllStudents()
        {
            var role = _facultyDbContext.Roles.SingleOrDefault(m => m.Name == "student");


            var userIds = role.Users
                .Select(y => y.UserId)
                .ToList();

            var entityStudents = _facultyDbContext.Users
                .Include(us => us.Scourses)
                .Where(us => userIds.Contains(us.Id))
                .ToList();
            var resultStudents = entityStudents.AsEnumerable().Select(x => x.Map()).ToList();
            return resultStudents;
        }

        public bool AddUser(User user, string role, string password)
        {
            var userManager = new AppUserManager(new UserStore<AppUser>(_facultyDbContext));
            var appUser = new AppUser {Email = user.Email, UserName = user.Name};
            var passwordUser = password;
            var resultTeacher = userManager.Create(appUser, passwordUser);
            if (resultTeacher.Succeeded)
            {
                userManager.AddToRole(appUser.Id, role);
                return true;
            }


            return false;
        }

        public bool DeleteUser(string email)
        {
            var userManager = new AppUserManager(new UserStore<AppUser>(_facultyDbContext));


            foreach (var course in _facultyDbContext.Courses.Include(x => x.Teacher))
                if (course.Teacher != null && course.Teacher.Email == email)
                    course.Teacher = null;
            var user = userManager.FindByName(email);
            userManager.Delete(user);
            return false;
        }
    }
}