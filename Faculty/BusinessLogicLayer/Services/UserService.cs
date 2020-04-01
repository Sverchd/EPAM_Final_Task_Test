using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
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

        public List<User> GetAllStudents()
        {
            var students = _userRepository.GetAllStudents();
            return students;
        }

        public bool AddTeacher(User teacher, string password)
        {
            var result = _userRepository.AddUser(teacher, "teacher", password);
            return result;
        }

        //List<Course> GetFilteredCoursesByTheme(Theme theme);
        public bool DeleteTeacher(string email)
        {
            var result = _userRepository.DeleteUser(email);
            return result;
        }

        public User GetTeacherByEmail(string email)
        {
            var users = _userRepository.GetAllTeachers();
            var teacher = users.Where(x => x.Email == email).FirstOrDefault();
            return teacher;
        }

        public User GetStudentByEmail(string email)
        {
            var students = _userRepository.GetAllStudents();
            var student = students.Where(x => x.Email == email).FirstOrDefault();
            return student;
        }
    }
}