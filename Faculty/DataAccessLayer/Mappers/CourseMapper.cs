using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Models;
using DataAccessLayer.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer.Mappers
{
    public static class CourseMapper
    {
        public static Course Map(this CourseEntity courseEntity)
        {
            var resultCourse = courseEntity.MapFlat();
            resultCourse.theme = courseEntity.theme.Map();
            if(courseEntity.Teacher!=null)
            resultCourse.teacher = courseEntity.Teacher.MapFlat();
            resultCourse.students = courseEntity.students.Select(x => x.MapFlat()).ToList();
            //var courses = new List<Course>();
            //foreach (var ecourse in courseEntity.Teacher.courses)
            //{
            //    courses.Add(new Course(ecourse.CourseEntityId, new Theme(ecourse.theme.ThemeEntityId, ecourse.theme.Name), ecourse.name, ecourse.start, ecourse.end,ecourse.Teacher.Map()));
            //}
            //if (courseEntity.Teacher != null)
            //{
            //
             //   resultCourse.teacher = new User(courseEntity.Teacher.Email, courseEntity.Teacher.Roles.ToString(),courses);
            //}

            //var students = new List<User>();
            //foreach (var entityStudent in courseEntity.students)
            //{
            //    var coursesStudent = new List<Course>();
            //    foreach (var ecourse in entityStudent.courses)
            //    {
            //        coursesStudent.Add(new Course(ecourse.CourseEntityId, new Theme(ecourse.theme.ThemeEntityId, ecourse.theme.Name), ecourse.name, ecourse.start, ecourse.end));
            //    }
            //    students.Add(new User(entityStudent.Email,entityStudent.Roles.ToString(),coursesStudent));
            //}
            //resultCourse.students = courseEntity.students.Select(x => x.Map()).ToList();
            //var resultCourse = new Course
            //{
            //    name = courseEntity.name,
            //    CourseId = courseEntity.CourseEntityId,
            //    theme = courseEntity.theme.Map(),
            //    start = courseEntity.start,
            //    end = courseEntity.end,
            //    
            //    teacher = courseEntity.Teacher.Map(),
            //    students = courseEntity.students.Select(x=>x.Map()).ToList()
            //};
            return resultCourse;
        }

        public static Course MapFlat(this CourseEntity courseEntity)
        {
            var resultCourse = new Course();
            resultCourse.name = courseEntity.name;
            resultCourse.CourseId = courseEntity.CourseEntityId;
            resultCourse.start = courseEntity.start;
            resultCourse.end = courseEntity.end;
            return resultCourse;
        }
    }
    public static class CourseEntityMapper
    {
        public static CourseEntity Map(this Course course)
        {
            var resultCourse = course.MapFlat();
            resultCourse.theme = course.theme.Map();
            resultCourse.Teacher = course.teacher.MapFlat();
            resultCourse.students = course.students.Select(x => x.MapFlat()).ToList();
            //var courses = new List<Course>();
            //foreach (var ecourse in courseEntity.Teacher.courses)
            //{
            //    courses.Add(new Course(ecourse.CourseEntityId, new Theme(ecourse.theme.ThemeEntityId, ecourse.theme.Name), ecourse.name, ecourse.start, ecourse.end,ecourse.Teacher.Map()));
            //}
            //if (courseEntity.Teacher != null)
            //{
            //
            //   resultCourse.teacher = new User(courseEntity.Teacher.Email, courseEntity.Teacher.Roles.ToString(),courses);
            //}

            //var students = new List<User>();
            //foreach (var entityStudent in courseEntity.students)
            //{
            //    var coursesStudent = new List<Course>();
            //    foreach (var ecourse in entityStudent.courses)
            //    {
            //        coursesStudent.Add(new Course(ecourse.CourseEntityId, new Theme(ecourse.theme.ThemeEntityId, ecourse.theme.Name), ecourse.name, ecourse.start, ecourse.end));
            //    }
            //    students.Add(new User(entityStudent.Email,entityStudent.Roles.ToString(),coursesStudent));
            //}
            //resultCourse.students = courseEntity.students.Select(x => x.Map()).ToList();
            //var resultCourse = new Course
            //{
            //    name = courseEntity.name,
            //    CourseId = courseEntity.CourseEntityId,
            //    theme = courseEntity.theme.Map(),
            //    start = courseEntity.start,
            //    end = courseEntity.end,
            //    
            //    teacher = courseEntity.Teacher.Map(),
            //    students = courseEntity.students.Select(x=>x.Map()).ToList()
            //};
            return resultCourse;
        }
        //Error no 
        public static CourseEntity MapFlat(this Course course)
        {
            var resultCourse = new CourseEntity();
            resultCourse.name = course.name;
            resultCourse.start = course.start;
            resultCourse.end = course.end;
            return resultCourse;
        }
    }
}
