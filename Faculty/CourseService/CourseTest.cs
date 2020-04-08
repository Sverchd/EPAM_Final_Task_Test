using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseService
{
    [TestClass]
    public class CourseTest
    {
        private readonly IThemeService _themeService;
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;

        public CourseTest()
        {
            var dbContext = new FacultyDbContext("FacultyContext");
            var themeRepository = new ThemeRepository(dbContext);
            var courseRepository = new CourseRepository(dbContext);
            var userRepository = new UserRepository(dbContext);
            _userService = new UserService(userRepository);
            _courseService = new BusinessLogicLayer.Services.CourseService(courseRepository, _userService);
            _themeService = new BusinessLogicLayer.Services.ThemeService(themeRepository, _courseService);

        }
        //int id, Theme theme, string name, DateTime start, DateTime end, User teacher
        [TestMethod]
        public void AddCourseTest()
        {
            var theme = new Theme("Test");
            var result = _themeService.AddTheme(theme);
            theme = _themeService.GetThemeByName("Test");
            var teacher = new User("AddTeacher@gmail.com", "", new List<Course>());
            _userService.AddTeacher(teacher, "Password_22");
            teacher = _userService.GetTeacherByEmail("AddTeacher@gmail.com");
            var expected = new Course(0, theme, "aCourse", new DateTime(2020, 4, 14), new DateTime(2020, 4, 18), teacher);
            _courseService.AddCourse(expected);
            var res =_courseService.GetCourseByName("aCourse");

            Assert.AreEqual(expected.Name,res.Name);
            Assert.AreEqual(expected.Start, res.Start);
            Assert.AreEqual(expected.End, res.End);
            Assert.AreEqual(expected.Theme.Name, res.Theme.Name);
            Assert.AreEqual(expected.Teacher.Name, res.Teacher.Name);
            _courseService.DeleteCourse(res.CourseId);
        }
        [TestMethod]
        public void GetAllCoursesTest()
        {
            var theme = new Theme("Test");
            var result = _themeService.AddTheme(theme);
            theme = _themeService.GetThemeByName("Test");
            var teacher = new User("AddTeacher@gmail.com", "", new List<Course>());
            _userService.AddTeacher(teacher, "Password_22");
            teacher = _userService.GetTeacherByEmail("AddTeacher@gmail.com");
            var expected = new Course(0, theme, "aCourse", new DateTime(2020, 4, 14), new DateTime(2020, 4, 18), teacher);
            _courseService.AddCourse(expected);
            var res = _courseService.GetAllCourses().OrderBy(x=>x.Name).ToList()[0];

            Assert.AreEqual(expected.Name, res.Name);
            Assert.AreEqual(expected.Start, res.Start);
            Assert.AreEqual(expected.End, res.End);
            Assert.AreEqual(expected.Theme.Name, res.Theme.Name);
            Assert.AreEqual(expected.Teacher.Name, res.Teacher.Name);
            
            _courseService.DeleteCourse(res.CourseId);
            _themeService.DeleteTheme(theme.ThemeId);
            _userService.DeleteUser(teacher.Email);
        }
        [TestMethod]
        public void GetCourseByIdTest()
        {
            var theme = new Theme("Test");
            var result = _themeService.AddTheme(theme);
            theme = _themeService.GetThemeByName("Test");
            var teacher = new User("AddTeacher@gmail.com", "", new List<Course>());
            _userService.AddTeacher(teacher, "Password_22");
            teacher = _userService.GetTeacherByEmail("AddTeacher@gmail.com");
            var expected = new Course(0, theme, "aCourse", new DateTime(2020, 4, 14), new DateTime(2020, 4, 18), teacher);
            var resId =_courseService.AddCourse(expected);
            var res = _courseService.GetCourseById(resId.CourseId);
            Assert.AreEqual(expected.Name, res.Name);
            Assert.AreEqual(expected.Start, res.Start);
            Assert.AreEqual(expected.End, res.End);
            Assert.AreEqual(expected.Theme.Name, res.Theme.Name);
            Assert.AreEqual(expected.Teacher.Name, res.Teacher.Name);
            _courseService.DeleteCourse(res.CourseId);
            _themeService.DeleteTheme(theme.ThemeId);
            _userService.DeleteUser(teacher.Email);
        }
        [TestMethod]
        public void GetCourseByNameTest()
        {
            var theme = new Theme("Test");
            var result = _themeService.AddTheme(theme);
            theme = _themeService.GetThemeByName("Test");
            var teacher = new User("AddTeacher@gmail.com", "", new List<Course>());
            _userService.AddTeacher(teacher, "Password_22");
            teacher = _userService.GetTeacherByEmail("AddTeacher@gmail.com");
            var expected = new Course(0, theme, "aCourse", new DateTime(2020, 4, 14), new DateTime(2020, 4, 18), teacher);
            var resId = _courseService.AddCourse(expected);
            var res = _courseService.GetCourseByName(resId.Name);
            Assert.AreEqual(expected.Name, res.Name);
            Assert.AreEqual(expected.Start, res.Start);
            Assert.AreEqual(expected.End, res.End);
            Assert.AreEqual(expected.Theme.Name, res.Theme.Name);
            Assert.AreEqual(expected.Teacher.Name, res.Teacher.Name);
            _courseService.DeleteCourse(res.CourseId);
            _themeService.DeleteTheme(theme.ThemeId);
            _userService.DeleteUser(teacher.Email);
        }
        [TestMethod]
        public void GetCoursesByThemeTest()
        {
            var theme = new Theme("Test");
            var result = _themeService.AddTheme(theme);
            theme = _themeService.GetThemeByName("Test");
            var teacher = new User("AddTeacher@gmail.com", "", new List<Course>());
            _userService.AddTeacher(teacher, "Password_22");
            teacher = _userService.GetTeacherByEmail("AddTeacher@gmail.com");
            var expected = new Course(0, theme, "aCourse", new DateTime(2020, 4, 14), new DateTime(2020, 4, 18), teacher);
            var resId = _courseService.AddCourse(expected);
            var res = _courseService.GetCoursesByTheme(theme.ThemeId)[0];
            Assert.AreEqual(expected.Name, res.Name);
            Assert.AreEqual(expected.Start, res.Start);
            Assert.AreEqual(expected.End, res.End);
            Assert.AreEqual(expected.Theme.Name, res.Theme.Name);
            Assert.AreEqual(expected.Teacher.Name, res.Teacher.Name);
            _courseService.DeleteCourse(res.CourseId);
            _themeService.DeleteTheme(theme.ThemeId);
            _userService.DeleteUser(teacher.Email);
        }
        [TestMethod]
        public void EditCourseTest()
        {
            var theme = new Theme("Test");
            _themeService.AddTheme(theme);
            theme = _themeService.GetThemeByName("Test");
            var teacher = new User("AddTeacher@gmail.com", "", new List<Course>());
            _userService.AddTeacher(teacher, "Password_22");
            teacher = _userService.GetTeacherByEmail("AddTeacher@gmail.com");
            var expected = new Course(0, theme, "aCourse", new DateTime(2020, 4, 14), new DateTime(2020, 4, 18), teacher);
            var resId = _courseService.AddCourse(expected);
            var course = _courseService.GetCourseById(resId.CourseId);
            course.Name = "aNewCourse";
            _courseService.EditCourse(course);
            var res = _courseService.GetCourseById(resId.CourseId);
            Assert.AreEqual(course.Name, res.Name);
            Assert.AreEqual(expected.Start, res.Start);
            Assert.AreEqual(expected.End, res.End);
            Assert.AreEqual(expected.Theme.Name, res.Theme.Name);
            Assert.AreEqual(expected.Teacher.Name, res.Teacher.Name);
            _courseService.DeleteCourse(res.CourseId);
            _themeService.DeleteTheme(theme.ThemeId);
            _userService.DeleteUser(teacher.Email);
        }
        [TestMethod]
        public void DeleteCourseTest()
        {
            var theme = new Theme("Test");
            _themeService.AddTheme(theme);
            theme = _themeService.GetThemeByName("Test");
            var teacher = new User("AddTeacher@gmail.com", "", new List<Course>());
            _userService.AddTeacher(teacher, "Password_22");
            teacher = _userService.GetTeacherByEmail("AddTeacher@gmail.com");
            var expected = new Course(0, theme, "aCourse", new DateTime(2020, 4, 14), new DateTime(2020, 4, 18), teacher);
            var resId = _courseService.AddCourse(expected);
            var course = _courseService.GetCourseById(resId.CourseId);
            _courseService.DeleteCourse(course.CourseId);
            var res = _courseService.GetCourseByName(resId.Name);
            Assert.IsNull(res);
            _themeService.DeleteTheme(theme.ThemeId);
            _userService.DeleteUser(teacher.Email);
        }

        [TestMethod]
        public void GetCoursesByTeacherTest()
        {
            var theme = new Theme("Test");
            _themeService.AddTheme(theme);
            theme = _themeService.GetThemeByName("Test");
            var teacher = new User("AddTeacher@gmail.com", "", new List<Course>());
            _userService.AddTeacher(teacher, "Password_22");
            teacher = _userService.GetTeacherByEmail("AddTeacher@gmail.com");
            var expected = new Course(0, theme, "aCourse", new DateTime(2020, 4, 14), new DateTime(2020, 4, 18), teacher);
            var course = _courseService.AddCourse(expected);
            var res = _courseService.GetCoursesByTeacher(teacher.Email)[0];
            
            Assert.AreEqual(course.Name, res.Name);
            Assert.AreEqual(expected.Start, res.Start);
            Assert.AreEqual(expected.End, res.End);
            Assert.AreEqual(expected.Theme.Name, res.Theme.Name);
            Assert.AreEqual(expected.Teacher.Name, res.Teacher.Name);
            _courseService.DeleteCourse(res.CourseId);
            _themeService.DeleteTheme(theme.ThemeId);
            _userService.DeleteUser(teacher.Email);
        }
        [TestMethod]
        public void GetCoursesByStudentTest()
        {
            var theme = new Theme("Test");
            _themeService.AddTheme(theme);
            theme = _themeService.GetThemeByName("Test");
            var teacher = new User("AddTeacher@gmail.com", "", new List<Course>());
            _userService.AddTeacher(teacher, "Password_22");
            teacher = _userService.GetTeacherByEmail("AddTeacher@gmail.com");
            var expected = new Course(0, theme, "aCourse", new DateTime(2020, 4, 14), new DateTime(2020, 4, 18), teacher);
            var course = _courseService.AddCourse(expected);
            var student = new User("AddTeacher@gmail.com", "", new List<Course>());
            _userService.AddStudent(student, "Password_22");
            _courseService.Register(course.CourseId, student.Name);
            var res =_courseService.GetCoursesByStudent(student.Email)[0];
            Assert.AreEqual(course.Name, res.Name);
            Assert.AreEqual(expected.Start, res.Start);
            Assert.AreEqual(expected.End, res.End);
            Assert.AreEqual(expected.Theme.Name, res.Theme.Name);
            Assert.AreEqual(expected.Teacher.Name, res.Teacher.Name);
            _courseService.DeleteCourse(res.CourseId);
            _themeService.DeleteTheme(theme.ThemeId);
            _userService.DeleteUser(teacher.Email);
            _userService.DeleteUser(student.Email);
        }
        [TestMethod]
        public void GetGradebookForCourseTest()
        {
            var theme = new Theme("Test");
            _themeService.AddTheme(theme);
            theme = _themeService.GetThemeByName("Test");
            var teacher = new User("AddTeacher@gmail.com", "", new List<Course>());
            _userService.AddTeacher(teacher, "Password_22");
            teacher = _userService.GetTeacherByEmail("AddTeacher@gmail.com");
            var expected = new Course(0, theme, "aCourse", new DateTime(2020, 4, 14), new DateTime(2020, 4, 18), teacher);
            var course = _courseService.AddCourse(expected);
            var student = new User("AddStudent@gmail.com", "", new List<Course>());
            _userService.AddStudent(student, "Password_22");
            _courseService.Register(course.CourseId, student.Name);
            var gradebook =_courseService.GetGradebookForCourse(course.CourseId);
            Assert.IsTrue(gradebook.FindAll(x=>x.StudentUsername=="AddStudent@gmail.com").Any());
            _courseService.DeleteCourse(course.CourseId);
            _themeService.DeleteTheme(theme.ThemeId);
            _userService.DeleteUser(teacher.Email);
            _userService.DeleteUser(student.Email);
        }
        [TestMethod]
        public void GetGradebookForStudentTest()
        {
            var theme = new Theme("Test");
            _themeService.AddTheme(theme);
            theme = _themeService.GetThemeByName("Test");
            var teacher = new User("AddTeacher@gmail.com", "", new List<Course>());
            _userService.AddTeacher(teacher, "Password_22");
            teacher = _userService.GetTeacherByEmail("AddTeacher@gmail.com");
            var expected = new Course(0, theme, "aCourse", new DateTime(2020, 4, 14), new DateTime(2020, 4, 18), teacher);
            var course = _courseService.AddCourse(expected);
            var student = new User("AddStudent@gmail.com", "", new List<Course>());
            _userService.AddStudent(student, "Password_22");
            _courseService.Register(course.CourseId, student.Name);
            var marks = new List<Mark>();
            marks.Add(new Mark(course.CourseId, student.Name, 12));
            _courseService.SaveGradebookForCourse(marks);
            
            var gradebook = _courseService.GetGradebookForStudent(student.Name);
            Assert.IsTrue(gradebook.FindAll(x => x.CourseId == course.CourseId).Any());
            _courseService.DeleteCourse(course.CourseId);
            _themeService.DeleteTheme(theme.ThemeId);
            _userService.DeleteUser(teacher.Email);
            _userService.DeleteUser(student.Email);
        }
    }
}
