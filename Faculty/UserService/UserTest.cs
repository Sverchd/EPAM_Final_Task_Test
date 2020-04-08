using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UserService
{
    [TestClass]
    public class UserTest
    {
        private readonly IThemeService _themeService;
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;

        public UserTest()
        {
            var dbContext = new FacultyDbContext("FacultyContext");
            var themeRepository = new ThemeRepository(dbContext);
            var courseRepository = new CourseRepository(dbContext);
            var userRepository = new UserRepository(dbContext);
            _userService = new BusinessLogicLayer.Services.UserService(userRepository);
            _courseService = new CourseService(courseRepository, _userService);
            _themeService = new BusinessLogicLayer.Services.ThemeService(themeRepository, _courseService);

        }
        [TestMethod]
        public void GetAllTeachersTest()
        {
            var user = new User("aTeacherTest@gmail.com", "",new List<Course>());
            _userService.AddTeacher(user, "Password_22");
            var teachers =_userService.GetAllTeachers();
            Assert.AreEqual(teachers.OrderBy(x=>x.Name).ToList()[0].Name,user.Name);
            _userService.DeleteUser("aTeacherTest@gmail.com");
        }
        [TestMethod]
        public void GetAllStudentsTest()
        {
            var user = new User("aStudentTest@gmail.com", "", new List<Course>());

            _userService.AddStudent(user, "Password_22");
            var students = _userService.GetAllStudents();
            Assert.AreEqual(students.OrderBy(x => x.Name).ToList()[0].Name, user.Name);
            _userService.DeleteUser("aStudentTest@gmail.com");
        }
        [TestMethod]
        public void GetAllBannedTest()
        {
            var user = new User("AddUser@gmail.com", "", new List<Course>());
            var student =_userService.AddStudent(user, "Password_22");
            _userService.Ban(student.Name);
            var banned = _userService.GetAllBanned();
            Assert.AreEqual( user.Name, banned.OrderBy(x => x.Name).ToList()[0].Name);
            _userService.DeleteUser("AddUser@gmail.com");
        }
        [TestMethod]
        public void AddTeacherTest()
        {
            var user = new User("AddTeacher@gmail.com", "", new List<Course>());
            _userService.AddTeacher(user, "Password_22");
            var teachers = _userService.GetAllTeachers();
            Assert.AreEqual(teachers.OrderBy(x => x.Name).ToList()[0].Name, user.Name);
            _userService.DeleteUser("AddTeacher@gmail.com");
        }
        [TestMethod]
        public void AddStudentTest()
        {
            var user = new User("aStudentTest@gmail.com", "", new List<Course>());
            _userService.AddStudent(user, "Password_22");
            var students = _userService.GetAllStudents();
            Assert.AreEqual(students.OrderBy(x => x.Name).ToList()[0].Name, user.Name);
            _userService.DeleteUser("aStudentTest@gmail.com");

        }
        [TestMethod]
        public void DeleteUserTest()
        {
            var user = new User("StudentTest@gmail.com", "", new List<Course>());
            _userService.AddStudent(user, "Password_22");
            _userService.DeleteUser("StudentTest@gmail.com");
            var res =_userService.GetStudentByEmail("StudentTest@gmail.com");
            Assert.AreEqual(null,res);
        }
        [TestMethod]
        public void GetTeacherByEmailTest()
        {
            var user = new User("AddTeacher@gmail.com", "", new List<Course>());
            _userService.AddTeacher(user, "Password_22");
            var teacher = _userService.GetTeacherByEmail("AddTeacher@gmail.com");
            Assert.AreEqual(teacher.Name, user.Name);
            _userService.DeleteUser("AddTeacher@gmail.com");
        }
        [TestMethod]
        public void GetStudentByEmailTest()
        {
            var user = new User("aStudentTest@gmail.com", "", new List<Course>());
            _userService.AddStudent(user, "Password_22");
            var student = _userService.GetStudentByEmail("aStudentTest@gmail.com");
            Assert.AreEqual(student.Name, user.Name);
            _userService.DeleteUser("aStudentTest@gmail.com");
        }

        [TestMethod]
        public void BanTest()
        {
            var user = new User("aStudentTest@gmail.com", "", new List<Course>());
            _userService.AddStudent(user, "Password_22");
            _userService.Ban("aStudentTest@gmail.com");
            var res = _userService.GetStudentByEmail("aStudentTest@gmail.com");
            Assert.AreEqual(null,res);
            _userService.DeleteUser("aStudentTest@gmail.com");
        }
        [TestMethod]
        public void ActivateTest()
        {
            var user = new User("aStudentTest@gmail.com", "", new List<Course>());
            _userService.AddStudent(user, "Password_22");
            _userService.Ban("aStudentTest@gmail.com");
            _userService.Activate("aStudentTest@gmail.com");
            var res = _userService.GetStudentByEmail("aStudentTest@gmail.com");
            Assert.AreEqual(user.Name, res.Name);
            _userService.DeleteUser("aStudentTest@gmail.com");
        }
    }
}
