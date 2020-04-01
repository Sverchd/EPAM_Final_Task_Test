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

        public List<Theme> GetAllThemes()
        {
            var themes = _themeRepository.GetAllThemes();
            return themes;
        }

        public bool Edit(Theme theme)
        {
            _themeRepository.Edit(theme);
            return true;
        }

        public bool AddTheme(Theme theme)
        {
            _themeRepository.GetAllThemes();
            //TODO: Get all themes
            //TODO: Check if exists
            //TODO: if not exists - delete

            var result = _themeRepository.AddTheme(theme);
            return result;
        }

        public Theme GeThemeById(int id)
        {
            var theme = _themeRepository.GetAllThemes().Where(x => x.ThemeId == id).First();
            return theme;
        }

        public bool DeleteTheme(int themeId)
        {
            var res = _themeRepository.DeleteTheme(themeId);
            return res;
        }
    }
}