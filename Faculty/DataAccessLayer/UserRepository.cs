using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using DataAccessLayer.Context;
using DataAccessLayer.Managers;
using DataAccessLayer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer
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
            var teachers = new List<User>();
            
            foreach (var eteacher in EnityTeachers)
            {
                var entityTeacher = _facultyDbContext.Users.Include(x=>x.courses.Select(y=>y.theme)).SingleOrDefault(u => u.Id == eteacher.UserId);
                var courses = new List<Course>();
                foreach (var ecourse in entityTeacher.courses)
                {
                    courses.Add(new Course(ecourse.CourseEntityId, new Theme(ecourse.theme.ThemeEntityId, ecourse.theme.Name), ecourse.name, ecourse.start, ecourse.end));
                }
                teachers.Add(new User(entityTeacher.Email,entityTeacher.Roles.ToString(),courses));
            }

            return teachers;
        }

        public bool AddUser(User user)
        {
            return false;
        }

        public bool DeleteUser(string email)
        {
            var userManager = new AppUserManager(new UserStore<AppUser>(_facultyDbContext));
            foreach (var course in _facultyDbContext.Courses.Include("Teacher"))
            {
                if (course.Teacher == null)
                    continue;
                if (course.Teacher.Email==email)
                {
                    course.Teacher = null;
                }
            }
            //_facultyDbContext.Courses.Where(c => c.Teacher.Email == email).ToList();
            var user = userManager.FindByName(email); 
            userManager.Delete(user);
            return false;
        }
    }
}
