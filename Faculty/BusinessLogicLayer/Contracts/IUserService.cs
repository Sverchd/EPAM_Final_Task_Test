using System.Collections.Generic;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Contracts
{
    public interface IUserService
    {
        List<User> GetAllTeachers();

        bool AddTeacher(User teacher, string password);

        bool DeleteTeacher(string email);
        User GetTeacherByEmail(string email);
        List<User> GetAllStudents();
        User GetStudentByEmail(string email);
    }
}