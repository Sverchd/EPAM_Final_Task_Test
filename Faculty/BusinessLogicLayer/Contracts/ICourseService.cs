using System.Collections.Generic;
using System.Data.Entity.Core;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Contracts
{
    public interface ICourseService
    {
        List<Course> GetAllCourses();
        List<Course> GetCoursesByTheme(int themeId);
        Course AddCourse(Course course);
        Course GetCourseById(int id);
        Course EditCourse(Course course);
        bool DeleteCourse(int courseId);
        List<Course> GetCoursesByTeacher(string email);
        int Register(int courseId, string username);
        List<Course> GetCoursesByStudent(string email);
        List<Mark> GetGradebookForCourse(int courseId);
        List<Mark> GetGradebookForStudent(string username);
        List<Mark> SaveGradebookForCourse(List<Mark> gradebook);
        Course GetCourseByName(string name);
    }
}