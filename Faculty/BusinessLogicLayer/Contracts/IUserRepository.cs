using System.Collections.Generic;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Contracts
{
    public interface IUserRepository
    {
        List<User> GetAllTeachers();
        List<User> GetAllStudents();
        List<User> GetAllBanned();
        User AddUser(User user, string role, string password);
        bool DeleteUser(string email);
        User Ban(string username);
        User Activate(string username);
    }
}