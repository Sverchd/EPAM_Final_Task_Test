﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Models;
using DataAccessLayer.Context;
using DataAccessLayer.Managers;
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
        ///     Method adds provided course to context
        /// </summary>
        /// <param name="course">provided course that need to be added to context</param>
        /// <returns></returns>
        public Course AddCourse(Course course)
        {
            var user = _facultyDbContext.Users.Include(x => x.Courses)
                .FirstOrDefault(x => x.Email == course.Teacher.Email);
            var theme = _facultyDbContext.Themes
                .SingleOrDefault(x => x.ThemeEntityId == course.Theme.ThemeId);
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
                .FirstOrDefault(x => x.Email == course.Teacher.Email);
            if (entityCourse == null || newtTeacher == null)
                return null;
            if (entityCourse.Teacher == null || newtTeacher.Email != entityCourse.Teacher.Email)
            {
                entityCourse.Teacher =
                    _facultyDbContext.Users.FirstOrDefault(x => x.Email == course.Teacher.Email);
                newtTeacher.Courses.Add(entityCourse);
            }
            entityCourse.Name = course.Name;
            entityCourse.Start = course.Start;
            entityCourse.End = course.End;
            entityCourse.Theme = _facultyDbContext.Themes
                .FirstOrDefault(x => x.ThemeEntityId == course.Theme.ThemeId);
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
            
            var marks = _facultyDbContext.Marks.Where(m => m.Course.CourseEntityId == courseId);
            _facultyDbContext.Marks.RemoveRange(marks);
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
            var student = _facultyDbContext.Users.Include(u => u.Scourses)
                .SingleOrDefault(s => s.UserName == username);
            var course = _facultyDbContext.Courses.Include(c => c.Students)
                .SingleOrDefault(c => c.CourseEntityId == courseId);
            student.Scourses.Add(course);
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
        /// <summary>
        /// Method saves all marks to context
        /// </summary>
        /// <param name="marks">list of marks</param>
        /// <returns>list of saved marks</returns>
        public List<Mark> SaveMarks(List<Mark> marks)
        {
            foreach (var mark in _facultyDbContext.Marks)
                _facultyDbContext.Marks.Remove(mark);
            _facultyDbContext.SaveChanges();
            foreach (var newMark in marks)
            {
                var course = _facultyDbContext.Courses
                    .SingleOrDefault(x => x.CourseEntityId == newMark.CourseId);
                var student = _facultyDbContext.Users
                    .SingleOrDefault(x => x.UserName == newMark.StudentUsername);
                _facultyDbContext.Marks.Add(new MarkEntity(course, student, newMark.Grade));
            }

            _facultyDbContext.SaveChanges();
            var newMarks = _facultyDbContext.Marks.Include(m => m.Course).Include(m => m.Student).ToList()
                .Select(x => x.Map()).ToList();
                
            return newMarks;
        }


    }
}