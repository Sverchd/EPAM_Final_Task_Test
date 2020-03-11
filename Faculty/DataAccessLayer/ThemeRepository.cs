using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using BusinessLogicLayer;
using DataAccessLayer.Context;
using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public class ThemeRepository : IThemeRepository
    {
        private readonly FacultyDbContext _facultyDbContext;

        public ThemeRepository(FacultyDbContext facultyDbContext)
        {
            _facultyDbContext = facultyDbContext;
        }

        public List<Theme> GetAllThemes()
        {
            var datalist = _facultyDbContext.Themes.ToList();

            var themes = new List<Theme>();

            foreach (var themeEntity in datalist)
            {
                
                themes.Add(new Theme(themeEntity.ThemeEntityId,themeEntity.Name));
            }

            return themes;
        }

        public bool AddTheme(Theme theme)
        {
            var entityTheme = new ThemeEntity(theme.Name);
            var themeList = _facultyDbContext.Themes.Select(m => m.Name).Contains(theme.Name);
            if (!themeList)
            {
                _facultyDbContext.Themes.Add(entityTheme);
                _facultyDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeleteTheme(int themeId)
        {
           var theme= _facultyDbContext.Themes.Where(i => i.ThemeEntityId == themeId).First();

           _facultyDbContext.Themes.Remove(theme);
           _facultyDbContext.SaveChanges();
            return true;
        }
        //public List<Course> GetFilteredCourses(Func<Course., bool> predicate)
        //{
        //    var coursEntity = _facultyDbContext.Courses.Where(predicate).ToList();
        //    return coursEntity;
        //}
        //public List<T> GetFilteredEntities<T>(Func<T, bool> predicate) where T : class
        //{
         //   List<T> entityList = null;
         //   entityList = _facultyDbContext.Set<T>().Where(predicate).ToList();
         //   return entityList;
        //}
    }
}