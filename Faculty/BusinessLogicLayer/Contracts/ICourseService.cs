using System.Collections.Generic;
using System.Data.Entity.Core;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Contracts
{
    public interface ICourseService
    {
        List<Course> GetAllCourses();
        List<Course> GetCoursesByTheme(Theme theme);
        bool AddCourse(Course course);
        Course GetCourseById(int id);
        bool EditCourse(Course course);
        bool DeleteCourse(int courseId);
        List<Course> GetCoursesByTeacher(string email);
        int Register(int courseId, string username);
        List<Course> GetCoursesByStudent(string email);
        List<Mark> GetGradebookForCourse(int courseId);
    }
}