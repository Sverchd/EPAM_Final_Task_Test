using System.Collections.Generic;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Contracts
{
    public interface ICourseRepository
    {
        List<Course> GetAllCourses();
        List<Course> GetCoursesByTheme(Theme theme);
        bool AddCourse(Course course);
        Course GetCourseById(int id);
        bool EditCourse(Course course);
        bool DeleteCourse(int courseId);
        bool Register(int courseId, string username);
        List<Mark> GetAllMarks();
    }
}