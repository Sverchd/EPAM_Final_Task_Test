using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Models;
using DataAccessLayer.Context;
using DataAccessLayer.Mappers;
using Microsoft.AspNet.Identity;

namespace DataAccessLayer.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private FacultyDbContext _facultyDbContext;

        public CourseRepository(FacultyDbContext facultyDbContext)
        {
            _facultyDbContext = facultyDbContext;
        }
        public List<Course> GetAllCourses()
        {
                var datalist = _facultyDbContext.Courses.Include(x=>x.theme).Include(x => x.Teacher).Include(x=>x.students).ToList();
                var courses = datalist.Select(x => x.Map()).ToList();

            //TODO: use mapper
            //foreach (var courseEntity in datalist)
            //    {
            //        courses.Add(new Course(courseEntity.CourseEntityId,new Theme(courseEntity.theme.ThemeEntityId,courseEntity.theme.Name ), courseEntity.name, courseEntity.start, courseEntity.end));
            //        
            //    }
            //    
                return courses;
        }

        public List<Course> GetCoursesByTheme(Theme theme)
        {
            var entityCourses = _facultyDbContext.Courses
                .Include(x => x.theme).Include(x=>x.students).Include(x=>x.Teacher)
                .Where(x => x.theme.Name == theme.Name).ToList();

            var courses = entityCourses.Select(x => x.Map()).ToList();

            return courses;
        }

        public bool AddCourse(Course course)
        {
            
            var user = _facultyDbContext.Users.Include(x => x.courses).Where(x => x.Email == course.teacher.Email)
                .FirstOrDefault();
            var entityCourse = course.Map();
            entityCourse.Teacher = user;
                user.courses.Add(entityCourse);
            _facultyDbContext.SaveChanges();
            return true;
        }

        public Course GetCourseById(int id)
        {
            var course = _facultyDbContext.Courses.Include(x=>x.theme).Include(x=>x.Teacher).Include(x=>x.students).Where(x => x.CourseEntityId == id).FirstOrDefault();
            return course.Map();
        }

        public bool EditCourse(Course course)
        {
            var entityCourse = _facultyDbContext.Courses.Include(x=>x.Teacher).Where(x => x.CourseEntityId == course.CourseId).FirstOrDefault();
            entityCourse.name = course.name;
            var newtTeacher =  _facultyDbContext.Users.Include(x=>x.courses).Where(x => x.Email == course.teacher.Email).FirstOrDefault();
            if (entityCourse.Teacher == null || newtTeacher.Email != entityCourse.Teacher.Email)
            {
                entityCourse.Teacher = _facultyDbContext.Users.Where(x => x.Email == course.teacher.Email).FirstOrDefault();
                newtTeacher.courses.Add(entityCourse);
            }
            
            entityCourse.start = course.start;
            entityCourse.end = course.end;
            entityCourse.theme = _facultyDbContext.Themes.Where(x => x.ThemeEntityId == course.theme.ThemeId)
                .FirstOrDefault();
            _facultyDbContext.SaveChanges();
            return true;
        }

        public bool DeleteCourse(int courseId)
        {
            var course = _facultyDbContext.Courses.Include(x=>x.Teacher).Include(x=>x.students).Where(x => x.CourseEntityId == courseId).FirstOrDefault();
            var usersWithCourse = _facultyDbContext.Users.Include(x=>x.courses).Where(x => x.courses.Contains(course));
            
            usersWithCourse.Select(x => x.courses.Remove(course));
            
            //var usersWithCourse =_facultyDbContext.Users.Select(x => x.courses.Where(y => y.CourseEntityId == courseId));
            //_facultyDbContext.Users.Where(x=>x.courses.FindAll(x=>x.CourseEntityId==courseId).)
            _facultyDbContext.Courses.Remove(course);
            _facultyDbContext.SaveChanges();
            return true;
        }

        public bool Register(int courseId, string username)
        {
            var student = _facultyDbContext.Users.Include(u=>u.courses).Where(s => s.UserName == username).SingleOrDefault();
            var course = _facultyDbContext.Courses.Include(c=>c.students).Where(c => c.CourseEntityId == courseId).SingleOrDefault();
            student.courses.Add(course);
            course.students.Add(student);
            _facultyDbContext.SaveChanges();
            return true;
        }
    }
}
