using System.Collections.Generic;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Contracts
{
    public interface ICourseRepository
    {
        List<Course> GetAllCourses();
        Course AddCourse(Course course);
        Course GetCourseById(int id);
        Course EditCourse(Course course);
        bool DeleteCourse(int courseId);
        bool Register(int courseId, string username);
        List<Mark> GetAllMarks();
        List<Mark> SaveMarks(List<Mark> marks);
    }
}