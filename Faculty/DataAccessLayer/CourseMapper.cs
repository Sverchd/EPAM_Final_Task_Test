using BusinessLogicLayer.Models;
using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public static class CourseMapper
    {
        public static Course Map(this CourseEntity courseEntity)
        {
            var resultCourse = new Course();
            resultCourse.name = courseEntity.name;
            resultCourse.CourseId = courseEntity.CourseEntityId;
            return resultCourse;
        }
    }
}
