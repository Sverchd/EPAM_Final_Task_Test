using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Models;
using DataAccessLayer.Context;
using DataAccessLayer.Mappers;

namespace DataAccessLayer.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly FacultyDbContext _facultyDbContext;

        public CourseRepository(FacultyDbContext facultyDbContext)
        {
            _facultyDbContext = facultyDbContext;
        }

        public List<Course> GetAllCourses()
        {
            var datalist = _facultyDbContext.Courses.Include(x => x.Theme).Include(x => x.Teacher)
                .Include(x => x.Students).ToList();
            var courses = datalist.Select(x => x.Map()).ToList();

            //TODO: use mapper
            //foreach (var courseEntity in datalist)
            //    {
            //        courses.Add(new Course(courseEntity.CourseEntityId,new Theme(courseEntity.Theme.ThemeEntityId,courseEntity.Theme.Name ), courseEntity.name, courseEntity.start, courseEntity.end));
            //        
            //    }
            //    
            return courses;
        }

        public List<Course> GetCoursesByTheme(Theme theme)
        {
            var entityCourses = _facultyDbContext.Courses
                .Include(x => x.Theme).Include(x => x.Students).Include(x => x.Teacher)
                .Where(x => x.Theme.Name == theme.Name).ToList();

            var courses = entityCourses.Select(x => x.Map()).ToList();

            return courses;
        }

        public bool AddCourse(Course course)
        {
            var user = _facultyDbContext.Users.Include(x => x.Courses).Where(x => x.Email == course.teacher.Email)
                .FirstOrDefault();
            var entityCourse = course.Map();
            entityCourse.Theme = _facultyDbContext.Themes.Where(x => x.ThemeEntityId == course.theme.ThemeId)
                .SingleOrDefault();
            entityCourse.Teacher = user;
            user.Courses.Add(entityCourse);
            _facultyDbContext.SaveChanges();
            return true;
        }

        public Course GetCourseById(int id)
        {
            var course = _facultyDbContext.Courses.Include(x => x.Theme).Include(x => x.Teacher)
                .Include(x => x.Students).Where(x => x.CourseEntityId == id).FirstOrDefault();
            return course.Map();
        }

        public bool EditCourse(Course course)
        {
            var entityCourse = _facultyDbContext.Courses.Include(x => x.Teacher)
                .Where(x => x.CourseEntityId == course.CourseId).FirstOrDefault();
            entityCourse.Name = course.name;
            var newtTeacher = _facultyDbContext.Users.Include(x => x.Courses)
                .Where(x => x.Email == course.teacher.Email).FirstOrDefault();
            if (entityCourse.Teacher == null || newtTeacher.Email != entityCourse.Teacher.Email)
            {
                entityCourse.Teacher =
                    _facultyDbContext.Users.Where(x => x.Email == course.teacher.Email).FirstOrDefault();
                newtTeacher.Courses.Add(entityCourse);
            }

            entityCourse.Start = course.start;
            entityCourse.End = course.end;
            entityCourse.Theme = _facultyDbContext.Themes.Where(x => x.ThemeEntityId == course.theme.ThemeId)
                .FirstOrDefault();
            _facultyDbContext.SaveChanges();
            return true;
        }

        public bool DeleteCourse(int courseId)
        {
            var course = _facultyDbContext.Courses.Include(x => x.Teacher).Include(x => x.Students)
                .Where(x => x.CourseEntityId == courseId).FirstOrDefault();
            var usersWithCourse =
                _facultyDbContext.Users.Include(x => x.Courses).Where(x => x.Courses.Contains(course));

            usersWithCourse.Select(x => x.Courses.Remove(course));
            _facultyDbContext.Courses.Remove(course);
            _facultyDbContext.SaveChanges();
            return true;
        }

        public bool Register(int courseId, string username)
        {
            var student = _facultyDbContext.Users.Include(u => u.Courses).Where(s => s.UserName == username)
                .SingleOrDefault();
            var course = _facultyDbContext.Courses.Include(c => c.Students).Where(c => c.CourseEntityId == courseId)
                .SingleOrDefault();
            student.Courses.Add(course);
            course.Students.Add(student);
            _facultyDbContext.SaveChanges();
            return true;
        }

        public List<Mark> GetAllMarks()
        {
            var marks = _facultyDbContext.Marks.Include(m=>m.Course).Include(m=>m.Student).ToList().Select(m => m.Map()).ToList();

            return marks;
        }
    }
}