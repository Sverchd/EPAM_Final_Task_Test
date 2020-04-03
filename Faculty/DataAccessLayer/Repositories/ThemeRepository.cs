using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Models;
using DataAccessLayer.Context;
using DataAccessLayer.Mappers;

namespace DataAccessLayer.Repositories
{
    public class ThemeRepository : IThemeRepository
    {
        private readonly FacultyDbContext _facultyDbContext;

        public ThemeRepository(FacultyDbContext facultyDbContext)
        {
            _facultyDbContext = facultyDbContext;
        }

        /// <summary>
        ///     Method gets all themes from context
        /// </summary>
        /// <returns>list of themes</returns>
        public List<Theme> GetAllThemes()
        {
            var datalist = _facultyDbContext.Themes.ToList();
            var themes = datalist.Select(x => x.Map()).ToList();
            return themes;
        }

        /// <summary>
        ///     Method adds provided theme to context
        /// </summary>
        /// <param name="theme">provided theme</param>
        /// <returns></returns>
        public bool AddTheme(Theme theme)
        {
            var entityTheme = theme.Map();
            var existingTheme = _facultyDbContext.Themes.Select(m => m.Name).Contains(theme.Name);
            if (!existingTheme)
            {
                _facultyDbContext.Themes.Add(entityTheme);
                _facultyDbContext.SaveChanges();
            }

            return !existingTheme;
        }

        /// <summary>
        ///     Method removes selected theme from context
        /// </summary>
        /// <param name="themeId">id of selected theme</param>
        /// <returns></returns>
        public bool DeleteTheme(int themeId)
        {
            var theme = _facultyDbContext.Themes.FirstOrDefault(i => i.ThemeEntityId == themeId);

            _facultyDbContext.Themes.Remove(theme);
            _facultyDbContext.SaveChanges();
            return true;
        }

        /// <summary>
        ///     Method saves edited theme to context
        /// </summary>
        /// <param name="theme">edited theme</param>
        /// <returns></returns>
        public bool Edit(Theme theme)
        {
            var themes = _facultyDbContext.Themes.ToList();
            var entityTheme = _facultyDbContext.Themes.First(x => x.ThemeEntityId == theme.ThemeId);
            entityTheme.Name = theme.Name;
            _facultyDbContext.SaveChanges();
            return true;
        }
    }
}