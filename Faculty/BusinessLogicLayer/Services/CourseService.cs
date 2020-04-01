using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public List<Course> GetAllCourses()
        {
            var courses = _courseRepository.GetAllCourses().Where(c => c.theme.Name != null).ToList();
            courses.ForEach(x => x.length = (x.end - x.start).Days);
            return courses;
        }

        public List<Course> GetCoursesByTheme(Theme theme)
        {
            var courses = _courseRepository.GetAllCourses();
            courses.ForEach(x => x.length = (x.end - x.start).Days);
            var resultCourses = courses.Where(x => x.theme.Name == theme.Name).ToList();
            return resultCourses;
        }

        public bool AddCourse(Course course)
        {
            return _courseRepository.AddCourse(course);
        }

        public Course GetCourseById(int id)
        {
            return _courseRepository.GetCourseById(id);
        }

        public bool EditCourse(Course course)
        {
            return _courseRepository.EditCourse(course);
        }

        public bool DeleteCourse(int courseId)
        {
            return _courseRepository.DeleteCourse(courseId);
        }

        public List<Course> GetCoursesByTeacher(string email)
        {
            var courses = _courseRepository.GetAllCourses();
            var selectedCourses = courses.Where(x => x.teacher.Email == email).ToList();
            courses.ForEach(x => x.length = (x.end - x.start).Days);
            return selectedCourses;
        }

        public List<Course> GetCoursesByStudent(string email)
        {
            var courses = _courseRepository.GetAllCourses();
            var selectedCourses = courses.Where(c => c.students.Find(x => x.Name == email) != null).ToList();
            courses.ForEach(x => x.length = (x.end - x.start).Days);
            return selectedCourses;
        }

        public int Register(int CourseId, string username)
        {
            if (_courseRepository.GetCourseById(CourseId).students.Where(s => s.Name == username).Any()) return 1;

            var res = _courseRepository.Register(CourseId, username);
            if (res)
                return 0;
            return 2;
        }
    }
}