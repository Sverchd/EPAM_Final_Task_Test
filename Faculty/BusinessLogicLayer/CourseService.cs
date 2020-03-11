using System.Collections.Generic;

namespace BusinessLogicLayer
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
            var courses = _courseRepository.GetAllCourses();
            return courses;
        }

        public List<Course> GetCoursesByTheme(Theme theme)
        {
            var courses = _courseRepository.GetCoursesByTheme(theme);
            return courses;
        }
    }
}