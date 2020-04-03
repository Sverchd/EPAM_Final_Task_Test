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

        /// <summary>
        ///     Method gets all teachers from context
        /// </summary>
        /// <returns>list of teachers</returns>
        public List<User> GetAllTeachers()
        {
            var role = _facultyDbContext.Roles.SingleOrDefault(m => m.Name == "Teacher");
            var userIds = role.Users
                .Select(y => y.UserId)
                .ToList();
            var entityTeachers = _facultyDbContext.Users
                .Include(us => us.Courses)
                .Where(us => userIds.Contains(us.Id))
                .ToList();
            var resultTeachers = entityTeachers.AsEnumerable().Select(x => x.Map()).ToList();

            return resultTeachers;
        }

        /// <summary>
        ///     Method gets all students from context
        /// </summary>
        /// <returns>list of students</returns>
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

        /// <summary>
        ///     Method adds (registers) provided user to context
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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

        /// <summary>
        ///     Method removes selected user from context
        /// </summary>
        /// <param name="email">email of selected user</param>
        /// <returns></returns>
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