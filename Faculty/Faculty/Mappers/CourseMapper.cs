using System.Linq;
using BusinessLogicLayer.Models;
using Faculty.Models;

namespace Faculty.Mappers
{
    /// <summary>
    ///     CourseView to Course mapper class
    /// </summary>
    public static class CourseMapper
    {
        /// <summary>
        ///     Map method
        /// </summary>
        /// <param name="courseView">courseView instance</param>
        /// <returns>Course instance (for BLL)</returns>
        public static Course Map(this CourseView courseView)
        {
            var resultCourse = courseView.MapFlat();
            resultCourse.theme = courseView.theme.Map();
            resultCourse.students = courseView.students.Select(x => x.MapFlat()).ToList();
            resultCourse.teacher = courseView.teacher.MapFlat();
            return resultCourse;
        }

        /// <summary>
        ///     method for flat mapping (without complex properties)
        /// </summary>
        /// <param name="courseView">courseView instance</param>
        /// <returns>Course instance (for BLL)</returns>
        public static Course MapFlat(this CourseView courseView)
        {
            var resultCourse = new Course();
            resultCourse.name = courseView.name;
            resultCourse.CourseId = courseView.CourseEntityId;
            resultCourse.start = courseView.start;
            resultCourse.end = courseView.end;
            return resultCourse;
        }
    }

    /// <summary>
    ///     Course to CourseView mapper class
    /// </summary>
    public static class CourseViewMapper
    {
        /// <summary>
        ///     Map method
        /// </summary>
        /// <param name="course">course instance</param>
        /// <returns>CourseView instance (for Presentation)</returns>
        public static CourseView Map(this Course course)
        {
            var resultCourse = course.MapFlat();
            resultCourse.theme = course.theme.Map();
            resultCourse.students = course.students.Select(x => x.MapFlat()).ToList();
            resultCourse.teacher = course.teacher.MapFlat();
            return resultCourse;
        }

        /// <summary>
        ///     method for flat mapping (without complex properties)
        /// </summary>
        /// <param name="course">course instance</param>
        /// <returns>CourseView instance (for Presentation)</returns>
        public static CourseView MapFlat(this Course course)
        {
            var resultCourse = new CourseView();
            resultCourse.name = course.name;
            resultCourse.CourseEntityId = course.CourseId;
            resultCourse.start = course.start;
            resultCourse.end = course.end;
            resultCourse.length = course.length;
            return resultCourse;
        }
    }
}