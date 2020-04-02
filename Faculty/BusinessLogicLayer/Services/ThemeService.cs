using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services
{
    public class ThemeService : IThemeService
    {
        private readonly IThemeRepository _themeRepository;

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
        /// <returns></returns>
        public bool Edit(Theme theme)
        {
            _themeRepository.Edit(theme);
            return true;
        }

        /// <summary>
        ///     Method adds provided themes
        /// </summary>
        /// <param name="theme">Theme that needed to be added</param>
        /// <returns></returns>
        public bool AddTheme(Theme theme)
        {
            _themeRepository.GetAllThemes();
            //TODO: Get all themes
            //TODO: Check if exists
            //TODO: if not exists - delete

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
            var theme = _themeRepository.GetAllThemes().FirstOrDefault(x => x.ThemeId == id);
            return theme;
        }

        /// <summary>
        ///     Deletes theme with provided id
        /// </summary>
        /// <param name="themeId">id of selected theme</param>
        /// <returns></returns>
        public bool DeleteTheme(int themeId)
        {
            var res = _themeRepository.DeleteTheme(themeId);
            return res;
        }
    }
}