using System.Linq;
using BusinessLogicLayer.Models;
using Faculty.Models;


namespace Faculty.Mappers
{
    public static class CourseMapper
    {
        public static Course Map(this CourseView courseView)
        {
            var resultCourse = courseView.MapFlat();
            resultCourse.theme = courseView.theme.Map();
            resultCourse.students = courseView.students.Select(x => x.MapFlat()).ToList();
            resultCourse.teacher = courseView.teacher.MapFlat();
            return resultCourse;
        }
        
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
    public static class CourseViewMapper
    {
        public static CourseView Map(this Course course)
        {
            var resultCourse = course.MapFlat();
            resultCourse.theme = course.theme.Map();
            resultCourse.students = course.students.Select(x => x.MapFlat()).ToList();
            resultCourse.teacher = course.teacher.MapFlat();
            return resultCourse;
        }
        
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