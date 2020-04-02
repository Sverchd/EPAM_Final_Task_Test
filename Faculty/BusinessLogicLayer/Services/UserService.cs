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

        /// <summary>
        ///     Method gets all teachers from repository
        /// </summary>
        /// <returns>list of teachers</returns>
        public List<User> GetAllTeachers()
        {
            var teachers = _userRepository.GetAllTeachers();
            return teachers;
        }

        /// <summary>
        ///     Method gets all students from repository
        /// </summary>
        /// <returns>All students</returns>
        public List<User> GetAllStudents()
        {
            var students = _userRepository.GetAllStudents();
            return students;
        }

        /// <summary>
        ///     Method adds(register) provided teacher
        /// </summary>
        /// <param name="teacher">teacher instance</param>
        /// <param name="password">Passsword for teacher account</param>
        /// <returns></returns>
        public bool AddTeacher(User teacher, string password)
        {
            var result = _userRepository.AddUser(teacher, "teacher", password);
            return result;
        }

        /// <summary>
        ///     Method deletes teacher with provided email
        /// </summary>
        /// <param name="email">email of teacher that needs to be deleted</param>
        /// <returns></returns>
        public bool DeleteTeacher(string email)
        {
            var result = _userRepository.DeleteUser(email);
            return result;
        }

        /// <summary>
        ///     Methods gets teacher with provided email
        /// </summary>
        /// <param name="email">email of selected teacher</param>
        /// <returns>instance of user selected by email</returns>
        public User GetTeacherByEmail(string email)
        {
            var users = _userRepository.GetAllTeachers();
            var teacher = users.FirstOrDefault(x => x.Email == email);
            return teacher;
        }

        /// <summary>
        ///     Method gets student with provided email
        /// </summary>
        /// <param name="email">email of selected teacher</param>
        /// <returns>instance of user by selected email</returns>
        public User GetStudentByEmail(string email)
        {
            var students = _userRepository.GetAllStudents();
            var student = students.FirstOrDefault(x => x.Email == email);
            return student;
        }

        /// <summary>
        ///     Method gets students of provided course
        /// </summary>
        /// <param name="courseId">id of selected course</param>
        /// <returns>list of students of selected course</returns>
        public List<User> GetStudentsByCourse(int courseId)
        {
            var students = _userRepository.GetAllStudents();
            var selectedStudents = students.Where(s => s.Courses.Find(x => x.CourseId == courseId) != null).ToList();
            return selectedStudents;
        }
    }
}