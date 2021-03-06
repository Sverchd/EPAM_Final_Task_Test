﻿using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        ///     Constructor of user service
        /// </summary>
        /// <param name="userRepository">interface pf user repository</param>
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
        ///     Method gets all banned students from repository
        /// </summary>
        /// <returns>All banned</returns>
        public List<User> GetAllBanned()
        {
            var banned = _userRepository.GetAllBanned();
            return banned;
        }

        /// <summary>
        ///     Method adds(register) provided teacher
        /// </summary>
        /// <param name="teacher">teacher instance</param>
        /// <param name="password">Password for teacher account</param>
        /// <returns>User that was added</returns>
        public User AddTeacher(User teacher, string password)
        {
            if (_userRepository.GetAllTeachers().SingleOrDefault(x => x.Email == teacher.Email) == null)
            {
                return _userRepository.AddUser(teacher, "teacher", password);
            }
            return null;
        }

        public User AddStudent(User student, string password)
        {
            if (_userRepository.GetAllStudents().SingleOrDefault(x => x.Email == student.Email) == null)
            {
                return _userRepository.AddUser(student, "student", password);
            }
            return null;
        }
        /// <summary>
        ///     Method deletes teacher with provided email
        /// </summary>
        /// <param name="email">email of teacher that needs to be deleted</param>
        /// <returns>result of operation</returns>
        public bool DeleteUser(string email)
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
        /// <summary>
        /// Method bans selected student
        /// </summary>
        /// <param name="username">name of selected student</param>
        /// <returns>banned student</returns>
        public User Ban(string username)
        {
            var user = _userRepository.Ban(username);
            return user;
        }
        /// <summary>
        /// Method activates selected student
        /// </summary>
        /// <param name="username">name of selected student</param>
        /// <returns>activated student</returns>
        public User Activate(string username)
        {
            var user = _userRepository.Activate(username);
            return user;
        }
    }
}