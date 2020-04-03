using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Models;
using DataAccessLayer.Context;
using DataAccessLayer.Mappers;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly FacultyDbContext _facultyDbContext;

        public CourseRepository(FacultyDbContext facultyDbContext)
        {
            _facultyDbContext = facultyDbContext;
        }

        /// <summary>
        ///     Method gets all courses from context
        /// </summary>
        /// <returns>list of selected courses</returns>
        public List<Course> GetAllCourses()
        {
            var datalist = _facultyDbContext.Courses.Include(x => x.Theme).Include(x => x.Teacher)
                .Include(x => x.Students).ToList();
            var courses = datalist.Select(x => x.Map()).ToList();
            return courses;
        }

        /// <summary>
        ///     Method gets courses of selected theme from context
        /// </summary>
        /// <param name="theme">selected theme</param>
        /// <returns>list of selected themes</returns>
        public List<Course> GetCoursesByTheme(Theme theme)
        {
            var entityCourses = _facultyDbContext.Courses
                .Include(x => x.Theme).Include(x => x.Students).Include(x => x.Teacher)
                .Where(x => x.Theme.Name == theme.Name).ToList();
            var courses = entityCourses.Select(x => x.Map()).ToList();
            return courses;
        }

        /// <summary>
        ///     Method adds provided course to context
        /// </summary>
        /// <param name="course">provided course that need to be added to context</param>
        /// <returns></returns>
        //public bool AddCourse(Course course)
        //{
        //    var user = _facultyDbContext.Users.Include(x => x.Courses)
        //        .FirstOrDefault(x => x.Email == course.teacher.Email);
        //
        //    var entityCourse = course.Map();
        //    entityCourse.Theme = _facultyDbContext.Themes
        //        .SingleOrDefault(x => x.ThemeEntityId == course.theme.ThemeId);
        //    entityCourse.Teacher = user;
        //    user.Courses.Add(entityCourse);
        //    _facultyDbContext.SaveChanges();
        //    return true;
        //}



        /// <summary>
        ///     Method adds provided course to context
        /// </summary>
        /// <param name="course">provided course that need to be added to context</param>
        /// <returns></returns>
        public Course AddCourse(Course course)
        {
            var user = _facultyDbContext.Users.Include(x => x.Courses)
                .FirstOrDefault(x => x.Email == course.teacher.Email);
            var theme = _facultyDbContext.Themes
                .SingleOrDefault(x => x.ThemeEntityId == course.theme.ThemeId);
            if (user == null||theme==null)
                return null;
            var entityCourse = course.Map();
            entityCourse.Theme = theme;
            entityCourse.Teacher = user;
            user.Courses.Add(entityCourse);
            _facultyDbContext.SaveChanges();
            return entityCourse.Map();
        }

        /// <summary>
        ///     Method gets course with selected id from context
        /// </summary>
        /// <param name="id">id of needed courses</param>
        /// <returns>instance of needed course</returns>
        public Course GetCourseById(int id)
        {
            var course = _facultyDbContext.Courses.Include(x => x.Theme).Include(x => x.Teacher)
                .Include(x => x.Students).FirstOrDefault(x => x.CourseEntityId == id);
            return course.Map();
        }

        /// <summary>
        ///     Method saves edited course
        /// </summary>
        /// <param name="course">edited course that need to be saved</param>
        /// <returns></returns>
        public Course EditCourse(Course course)
        {
            var entityCourse = _facultyDbContext.Courses.Include(x => x.Teacher)
                .FirstOrDefault(x => x.CourseEntityId == course.CourseId);
            
            var newtTeacher = _facultyDbContext.Users.Include(x => x.Courses)
                .FirstOrDefault(x => x.Email == course.teacher.Email);
            if (entityCourse == null || newtTeacher == null)
                return null;
            if (entityCourse.Teacher == null || newtTeacher.Email != entityCourse.Teacher.Email)
            {
                entityCourse.Teacher =
                    _facultyDbContext.Users.FirstOrDefault(x => x.Email == course.teacher.Email);
                newtTeacher.Courses.Add(entityCourse);
            }
            entityCourse.Name = course.name;
            entityCourse.Start = course.start;
            entityCourse.End = course.end;
            entityCourse.Theme = _facultyDbContext.Themes
                .FirstOrDefault(x => x.ThemeEntityId == course.theme.ThemeId);
            _facultyDbContext.SaveChanges();
            return entityCourse.Map();
        }

        /// <summary>
        ///     Method removes selected course from context
        /// </summary>
        /// <param name="courseId">id of selected course</param>
        /// <returns></returns>
        public bool DeleteCourse(int courseId)
        {
            var course = _facultyDbContext.Courses.Include(x => x.Teacher).Include(x => x.Students)
                .FirstOrDefault(x => x.CourseEntityId == courseId);
            var usersWithCourse =
                _facultyDbContext.Users.Include(x => x.Courses).Where(x => x.Courses.Contains(course));
            usersWithCourse.Select(x => x.Courses.Remove(course));
            _facultyDbContext.Courses.Remove(course);
            _facultyDbContext.SaveChanges();
            return true;
        }

        /// <summary>
        ///     Method registers student for course
        /// </summary>
        /// <param name="courseId">Id of selected course</param>
        /// <param name="username">Id of selected student</param>
        /// <returns></returns>
        public bool Register(int courseId, string username)
        {
            var student = _facultyDbContext.Users.Include(u => u.Courses)
                .SingleOrDefault(s => s.UserName == username);
            var course = _facultyDbContext.Courses.Include(c => c.Students)
                .SingleOrDefault(c => c.CourseEntityId == courseId);
            student.Courses.Add(course);
            course.Students.Add(student);
            _facultyDbContext.SaveChanges();
            return true;
        }

        /// <summary>
        ///     Method gets all marks from context
        /// </summary>
        /// <returns></returns>
        public List<Mark> GetAllMarks()
        {
            var marks = _facultyDbContext.Marks.Include(m => m.Course).Include(m => m.Student).ToList()
                .Select(m => m.Map()).ToList();

            return marks;
        }

        public List<Mark> SaveMarks(List<Mark> marks)
        {
            var Oldmarks = _facultyDbContext.Marks.Include(m => m.Course).Include(m => m.Student).ToList()
                ;
            dynamic d;
            //marks.ForEach(x=>Oldmarks.Where(old=>old.Course.CourseEntityId==x.CourseId&&old.Student.Email==x.StudentUsername).ToList().ForEach(a=>a.Mark=x.Grade;\));
            foreach (var mark in _facultyDbContext.Marks)
                _facultyDbContext.Marks.Remove(mark);
            _facultyDbContext.SaveChanges();
            foreach (var newMark in marks)
            {
                var course = _facultyDbContext.Courses.Where(x => x.CourseEntityId == newMark.CourseId)
                    .SingleOrDefault();
                var student = _facultyDbContext.Users.Where(x => x.UserName == newMark.StudentUsername)
                    .SingleOrDefault();
                _facultyDbContext.Marks.Add(new MarkEntity(course, student, newMark.Grade));
            }

            _facultyDbContext.SaveChanges();
            //_facultyDbContext.Marks.Remove()
            var newMarks = _facultyDbContext.Marks.Include(m => m.Course).Include(m => m.Student).ToList()
                .Select(x => x.Map()).ToList();
                
            return newMarks;
        }
    }
}