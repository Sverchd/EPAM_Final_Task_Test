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
            resultCourse.Theme = courseView.Theme.Map();
            resultCourse.Students = courseView.Students.Select(x => x.MapFlat()).ToList();
            resultCourse.Teacher = courseView.teacher.MapFlat();
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
            resultCourse.Name = courseView.Name;
            resultCourse.CourseId = courseView.CourseEntityId;
            resultCourse.Start = courseView.Start;
            resultCourse.End = courseView.End;
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
            resultCourse.Theme = course.Theme.Map();
            resultCourse.Students = course.Students.Select(x => x.MapFlat()).ToList();
            resultCourse.teacher = course.Teacher.MapFlat();
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
            resultCourse.Name = course.Name;
            resultCourse.CourseEntityId = course.CourseId;
            resultCourse.Start = course.Start;
            resultCourse.End = course.End;
            resultCourse.Length = course.Length;
            return resultCourse;
        }
    }
}