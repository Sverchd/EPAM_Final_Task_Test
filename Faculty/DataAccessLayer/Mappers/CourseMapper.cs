using System.Linq;
using BusinessLogicLayer.Models;
using DataAccessLayer.Models;

namespace DataAccessLayer.Mappers
{
    /// <summary>
    ///     CourseEntity to course mapper class
    /// </summary>
    public static class CourseMapper
    {
        /// <summary>
        ///     map method
        /// </summary>
        /// <param name="courseEntity">entity course instance</param>
        /// <returns>course instance (for BLL)</returns>
        public static Course Map(this CourseEntity courseEntity)
        {
            var resultCourse = courseEntity.MapFlat();
            resultCourse.theme = courseEntity.theme.Map();
            if (courseEntity.Teacher != null)
                resultCourse.teacher = courseEntity.Teacher.MapFlat();
            resultCourse.students = courseEntity.students.Select(x => x.MapFlat()).ToList();

            return resultCourse;
        }

        /// <summary>
        ///     method for flat mapping (without complex properties)
        /// </summary>
        /// <param name="courseEntity">entity course instance</param>
        /// <returns>course instance (for BLL)</returns>
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

    /// <summary>
    ///     Course to CourseEntity mapper class
    /// </summary>
    public static class CourseEntityMapper
    {
        /// <summary>
        ///     map method
        /// </summary>
        /// <param name="course">course instance</param>
        /// <returns>instance of CourseEntity for (DAL)</returns>
        public static CourseEntity Map(this Course course)
        {
            var resultCourse = course.MapFlat();
            resultCourse.theme = course.theme.Map();
            resultCourse.Teacher = course.teacher.MapFlat();
            resultCourse.students = course.students.Select(x => x.MapFlat()).ToList();
            return resultCourse;
        }

        /// <summary>
        ///     method for flat mapping (without complex properties)
        /// </summary>
        /// <param name="course">course instance</param>
        /// <returns>instance of CourseEntity for (DAL)</returns>
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