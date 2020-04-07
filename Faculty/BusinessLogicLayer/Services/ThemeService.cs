using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services
{
    public class ThemeService : IThemeService
    {
        private readonly IThemeRepository _themeRepository;

        /// <summary>
        ///     Constructor of theme service
        /// </summary>
        /// <param name="themeRepository">interface of theme repository</param>
        public ThemeService(IThemeRepository themeRepository)
        {
            _themeRepository = themeRepository;
        }

        /// <summary>
        ///     Gets all themes from repository
        /// </summary>
        /// <returns>returns the list of all themes</returns>
        public List<Theme> GetAllThemes()
        {
            var themes = _themeRepository.GetAllThemes();
            return themes;
        }

        /// <summary>
        ///     Method saves edited theme
        /// </summary>
        /// <param name="theme">Edited theme</param>
        /// <returns>Theme that was edited</returns>
        public Theme Edit(Theme theme)
        {
            var oldTheme = GetThemeById(theme.ThemeId);
            var themeWithName = GetThemeByName(theme.Name);

            if (themeWithName == null || themeWithName.ThemeId == theme.ThemeId)
            {

                return _themeRepository.Edit(theme);
            }
            return null;
        }

        /// <summary>
        ///     Method adds provided themes
        /// </summary>
        /// <param name="theme">Theme that needed to be added</param>
        /// <returns>Theme that was added</returns>
        public Theme AddTheme(Theme theme)
        {
            var themes =_themeRepository.GetAllThemes();
            if (themes.SingleOrDefault(x => x.Name == theme.Name)!=null)
            {
                return null;
            }
            var result = _themeRepository.AddTheme(theme);
            return result;
        }

        /// <summary>
        ///     Method gets theme with provided id
        /// </summary>
        /// <param name="id">id of needed theme</param>
        /// <returns>theme with selected id</returns>
        public Theme GetThemeById(int id)
        {
            return _themeRepository.GetAllThemes().FirstOrDefault(x => x.ThemeId == id);
        }
        /// <summary>
        /// Method gets theme with provided name
        /// </summary>
        /// <param name="themeName"></param>
        /// <returns>theme with selected name</returns>
        public Theme GetThemeByName(string themeName)
        {
            return _themeRepository.GetAllThemes().FirstOrDefault(x => x.Name == themeName);
        }

        /// <summary>
        ///     Deletes theme with provided id
        /// </summary>
        /// <param name="themeId">id of selected theme</param>
        /// <returns>result of operation</returns>
        public bool DeleteTheme(int themeId)
        {
            var res = _themeRepository.DeleteTheme(themeId);
            return res;
        }
    }
}