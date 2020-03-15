using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IUserRepository
    {
        List<User> GetAllTeachers();

        bool AddUser(User user);

        bool DeleteUser(string email);
    }
}
