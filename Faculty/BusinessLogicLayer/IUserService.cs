using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IUserService
    {
        List<User> GetAllTeachers();

        bool AddTeacher(User teacher);
        //List<Course> GetFilteredCoursesByTheme(Theme theme);
        bool DeleteTeacher(string email);
    }
}
