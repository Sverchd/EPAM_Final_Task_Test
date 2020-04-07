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
            resultCourse.Theme = courseEntity.Theme.Map();
            if (courseEntity.Teacher != null)
                resultCourse.Teacher = courseEntity.Teacher.MapFlat();
            resultCourse.Students = courseEntity.Students.Select(x => x.MapFlat()).ToList();

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
            resultCourse.Name = courseEntity.Name;
            resultCourse.CourseId = courseEntity.CourseEntityId;
            resultCourse.Start = courseEntity.Start;
            resultCourse.End = courseEntity.End;
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
            resultCourse.Theme = course.Theme.Map();
            resultCourse.Teacher = course.Teacher.MapFlat();
            resultCourse.Students = course.Students.Select(x => x.MapFlat()).ToList();
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
            resultCourse.Name = course.Name;
            resultCourse.Start = course.Start;
            resultCourse.End = course.End;
            return resultCourse;
        }
    }
}