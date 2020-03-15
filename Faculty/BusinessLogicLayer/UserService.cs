using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public List<User> GetAllTeachers()
        {
            var teachers = _userRepository.GetAllTeachers();
            return teachers;
        }

        public bool AddTeacher(User teacher)
        {
            var result = _userRepository.AddUser(teacher);
            return result;
        }
        //List<Course> GetFilteredCoursesByTheme(Theme theme);
        public bool DeleteTeacher(string email)
        {
            var result = _userRepository.DeleteUser(email);
            return result;
        }
    }
}
