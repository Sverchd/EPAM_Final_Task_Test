﻿using System.Collections.Generic;
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
            var courses = _courseRepository.GetAllCourses().Where(c => c.theme.Name != null).ToList();
            courses.ForEach(x => x.length = (x.end - x.start).Days);
            return courses;
        }

        /// <summary>
        ///     Method gets all courses that belongs to requested theme
        /// </summary>
        /// <param name="theme">selected theme</param>
        /// <returns>Returns courses of selected theme</returns>
        public List<Course> GetCoursesByTheme(Theme theme)
        {
            var courses = _courseRepository.GetAllCourses();
            courses.ForEach(x => x.length = (x.end - x.start).Days);
            var resultCourses = courses.Where(x => x.theme.Name == theme.Name).ToList();
            return resultCourses;
        }

        /// <summary>
        ///     Methods adds a new course
        /// </summary>
        /// <param name="course">Course needed to add</param>
        /// <returns></returns>
        public bool AddCourse(Course course)
        {
            return _courseRepository.AddCourse(course);
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

        /// <summary>
        ///     Method updates provided course
        /// </summary>
        /// <param name="course">provided course</param>
        /// <returns></returns>
        public bool EditCourse(Course course)
        {
            return _courseRepository.EditCourse(course);
        }

        /// <summary>
        ///     Method deletes course with provided id
        /// </summary>
        /// <param name="courseId">id of course that needs to be removed</param>
        /// <returns></returns>
        public bool DeleteCourse(int courseId)
        {
            return _courseRepository.DeleteCourse(courseId);
        }

        /// <summary>
        ///     Method gets all courses of selected teacher
        /// </summary>
        /// <param name="email">email of selected teacher</param>
        /// <returns>courses of selected teacher</returns>
        public List<Course> GetCoursesByTeacher(string email)
        {
            var courses = _courseRepository.GetAllCourses();
            var selectedCourses = courses.Where(x => x.teacher.Email == email).ToList();
            courses.ForEach(x => x.length = (x.end - x.start).Days);
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
            var selectedCourses = courses.Where(c => c.students.Find(x => x.Name == email) != null).ToList();
            courses.ForEach(x => x.length = (x.end - x.start).Days);
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
            if (_courseRepository.GetCourseById(CourseId).students.Any(s => s.Name == username)) return 1;

            var res = _courseRepository.Register(CourseId, username);
            if (res)
                return 0;
            return 2;
        }


        /// <summary>
        ///     Method gets grades of course with provided id
        /// </summary>
        /// <param name="courseId">id of selected course</param>
        /// <returns>list of marks for selected course (gradebook)</returns>
        public List<Mark> GetGradebookForCourse(int courseId)
        {
            var gradebook = new Dictionary<User, int?>();
            var marks = _courseRepository.GetAllMarks().Where(m => m.Course.CourseId == courseId).ToList();
            var students = _userService.GetStudentsByCourse(courseId);
            foreach (var student in students)
                if (!marks.Where(m => m.Student.Name == student.Name).Any())
                    marks.Add(new Mark(student.Courses.Where(c => c.CourseId == courseId).SingleOrDefault(), student,
                        null));
            return marks;
        }
    }
}