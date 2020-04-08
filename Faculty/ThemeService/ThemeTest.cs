using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using System.Data.Entity;

namespace ThemeService
{
    [TestClass]
    public class ThemeTest
    {
        private readonly IThemeService _themeService;
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;

        public ThemeTest()
        {
            var dbContext = new FacultyDbContext("FacultyContext");

            var themeRepository = new ThemeRepository(dbContext);
            var courseRepository = new CourseRepository(dbContext);
            var userRepository = new UserRepository(dbContext);

            _userService = new UserService(userRepository);
            _courseService = new CourseService(courseRepository,_userService);
            _themeService = new BusinessLogicLayer.Services.ThemeService(themeRepository, _courseService);
            
        }
        [TestMethod]
        public void GetAllThemesTest()
        {
            
            var expected = new List<Theme>()
            {
                new Theme(1,"Science",1),
                new Theme(2,"IT",1),
                new Theme(3,"Language",0)
            };
            

                var res = _themeService.GetAllThemes();
                Assert.AreEqual(expected.Count, res.Count);
                res.OrderBy(x => x.ThemeId);
                expected.OrderBy(x => x.ThemeId);
                for (int i = 0; i < expected.Count; ++i)
                {
                    Assert.AreEqual(expected[i].Name, res[i].Name);
                }
        }

        [TestMethod]
        public void AddThemeTest()
        {
            var expected = new Theme( "Test");
            var result =_themeService.AddTheme(expected);
            Assert.AreEqual(expected.Name,result.Name);

            _themeService.DeleteTheme(result.ThemeId);
        }
        [TestMethod]
        public void EditTest()
        {
            var initial = new Theme("Theme");
            var expected = "ThemeEdit";
            _themeService.AddTheme(initial);
            var toedit = _themeService.GetThemeByName("Theme");
            toedit.Name = "ThemeEdit";
            var result = _themeService.Edit(toedit);
            Assert.AreEqual(expected, result.Name);
            _themeService.DeleteTheme(result.ThemeId);
        }

        [TestMethod]
        public void GetThemeByIdTest()
        {
            var initial = new Theme("Theme");
            var theme =_themeService.AddTheme(initial);
            var res = _themeService.GetThemeById(theme.ThemeId);
            Assert.AreEqual(initial.Name, res.Name);
            _themeService.DeleteTheme(res.ThemeId);
        }
        [TestMethod]
        public void GetThemeByNameTest()
        {
            var initial = new Theme("Theme");
            _themeService.AddTheme(initial);
            var res = _themeService.GetThemeByName("Theme");
            Assert.AreEqual(initial.Name, res.Name);
            _themeService.DeleteTheme(res.ThemeId);
        }
        [TestMethod]
        public void DeleteTest()
        {
            var initial = new Theme("Theme");
            var res =_themeService.AddTheme(initial);
            _themeService.DeleteTheme(res.ThemeId);
        }
    }
}
