using System.Linq;
using BusinessLogicLayer.Models;
using DataAccessLayer.Models;

namespace DataAccessLayer.Mappers
{
    public static class CourseMapper
    {
        public static Course Map(this CourseEntity courseEntity)
        {
            var resultCourse = courseEntity.MapFlat();
            resultCourse.theme = courseEntity.theme.Map();
            if (courseEntity.Teacher != null)
                resultCourse.teacher = courseEntity.Teacher.MapFlat();
            resultCourse.students = courseEntity.students.Select(x => x.MapFlat()).ToList();

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
            return resultCourse;
        }

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