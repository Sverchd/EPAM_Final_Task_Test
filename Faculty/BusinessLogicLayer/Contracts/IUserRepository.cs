using System.Collections.Generic;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Contracts
{
    public interface IUserRepository
    {
        List<User> GetAllTeachers();
        List<User> GetAllStudents();
        User AddUser(User user, string role, string password);
        bool DeleteUser(string email);
    }
}