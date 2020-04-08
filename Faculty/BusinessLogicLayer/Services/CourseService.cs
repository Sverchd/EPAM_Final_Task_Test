using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserService _userService;

        /// <summary>
        ///     Course service constructor
        /// </summary>
        /// <param name="courseRepository">course repository</param>
        /// <param name="userService">User service</param>
        public CourseService(ICourseRepository courseRepository, IUserService userService)
        {
            _courseRepository = courseRepository;
            _userService = userService;
        }

        /// <summary>
        ///     Method gets all courses
        /// </summary>
        /// <returns>All the courses</returns>
        public List<Course> GetAllCourses()
        {
            var courses = _courseRepository.GetAllCourses().Where(c => c.Theme.Name != null).ToList();
            courses.ForEach(x => x.Length = (x.End - x.Start).Days);
            return courses;
        }

        /// <summary>
        ///     Method gets all courses that belongs to requested theme
        /// </summary>
        /// <param name="themeId">selected theme</param>
        /// <returns>Returns courses of selected theme</returns>
        public List<Course> GetCoursesByTheme(int themeId)
        {
            var courses = _courseRepository.GetAllCourses();
            courses.ForEach(x => x.Length = (x.End - x.Start).Days);
            var resultCourses = courses.Where(x => x.Theme.ThemeId == themeId).ToList();
            return resultCourses;
        }

        /// <summary>
        ///     Methods adds a new course
        /// </summary>
        /// <param name="course">Course needed to add</param>
        /// <returns>Course that was created</returns>
        public Course AddCourse(Course course)
        {
            if (GetCourseByName(course.Name) != null)
            {
                return null;
            }
            else
            {
                return _courseRepository.AddCourse(course);
            }
        }

        /// <summary>
        ///     Method gets a course with selected id
        /// </summary>
        /// <param name="id">Id of needed course</param>
        /// <returns>Course with selected id</returns>
        public Course GetCourseById(int id)
        {
            return _courseRepository.GetCourseById(id);
        }

        public Course GetCourseByName(string name)
        {
            var courses = _courseRepository.GetAllCourses();
            var course = courses.SingleOrDefault(x => x.Name == name);
            return course;
        }
        /// <summary>
        ///     Method updates provided course
        /// </summary>
        /// <param name="course">provided course</param>
        /// <returns>Course that was edited</returns>
        public Course EditCourse(Course course)
        {
            var oldCourse = GetCourseById(course.CourseId);
            var courseWithName = GetCourseByName(course.Name);
            if (courseWithName==null||courseWithName.CourseId==course.CourseId)
            {
                var newCourse = _courseRepository.EditCourse(course);
                if (newCourse != null &&
                    newCourse.Name == course.Name &&
                    newCourse.Theme.ThemeId == course.Theme.ThemeId &&
                    newCourse.Start == course.Start &&
                    newCourse.End == course.End &&
                    newCourse.Teacher.Name == course.Teacher.Name)
                {
                    return newCourse;
                }
            }
            return null;
            }

        /// <summary>
        ///     Method deletes course with provided id
        /// </summary>
        /// <param name="courseId">id of course that needs to be removed</param>
        /// <returns>result of operation</returns>
        public bool DeleteCourse(int courseId)
        {
            var course = GetCourseById(courseId);
            if (course!=null)
            {
                _courseRepository.DeleteCourse(courseId);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Method gets all courses of selected teacher
        /// </summary>
        /// <param name="email">email of selected teacher</param>
        /// <returns>courses of selected teacher</returns>
        public List<Course> GetCoursesByTeacher(string email)
        {
            var courses = _courseRepository.GetAllCourses();
            var selectedCourses = courses.Where(x => x.Teacher.Email == email).ToList();
            courses.ForEach(x => x.Length = (x.End - x.Start).Days);
            return selectedCourses;
        }

        /// <summary>
        ///     Gets all courses of selected student
        /// </summary>
        /// <param name="email">email of selected teacher</param>
        /// <returns>courses of selected student</returns>
        public List<Course> GetCoursesByStudent(string email)
        {
            var courses = _courseRepository.GetAllCourses();
            var selectedCourses = courses.Where(c => c.Students.Find(x => x.Name == email) != null).ToList();
            courses.ForEach(x => x.Length = (x.End - x.Start).Days);
            return selectedCourses;
        }

        /// <summary>
        ///     Method registers student with provided username for course with provided id
        /// </summary>
        /// <param name="CourseId">id of course</param>
        /// <param name="username">username of student</param>
        /// <returns>code of result</returns>
        public int Register(int CourseId, string username)
        {
            if (_courseRepository.GetCourseById(CourseId).Students.Any(s => s.Name == username)) return 1;
            var course = GetCourseById(CourseId);
            if (course.Teacher.Name == null)
            {
                throw new Exception("No teacher");
            }
            var res = _courseRepository.Register(CourseId, username);
            
            
            if (res)
                return 0;
            return 2;
        }
        /// <summary>
        /// Method gets gradebook for selected course
        /// </summary>
        /// <param name="courseId">id of selected course</param>
        /// <returns>list of marks</returns>
        public List<Mark> GetGradebookForCourse(int courseId)
        {
            var marks = _courseRepository.GetAllMarks().Where(m => m.CourseId == courseId).ToList();
            var students = _userService.GetStudentsByCourse(courseId);
            foreach (var student in students)
                if (!marks.Any(m => m.StudentUsername == student.Name))
                    marks.Add(new Mark(courseId, student.Name,
                        null));
            return marks;
        }
        /// <summary>
        /// Method gets gradebook for selected student
        /// </summary>
        /// <param name="username">username of selected student</param>
        /// <returns>list of marks</returns>
        public List<Mark> GetGradebookForStudent(string username)
        {
            var marks = _courseRepository.GetAllMarks().Where(m => m.StudentUsername == username).ToList();
            return marks;
        }
        /// <summary>
        /// Method saves edited gradebook
        /// </summary>
        /// <param name="gradebook">edited gradebook</param>
        /// <returns>list of marks</returns>
        public List<Mark> SaveGradebookForCourse(List<Mark> gradebook)
        {
            var newgradebook =_courseRepository.SaveMarks(gradebook);
            return newgradebook;
        }

    }
}