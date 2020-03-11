using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
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

        public bool AddTheme(Theme theme)
        {
            var result =_themeRepository.AddTheme(theme);
            return result;
        }

        public bool DeleteTheme(int themeId)
        {
            var res = _themeRepository.DeleteTheme(themeId);
            return res;
        }
        //public List<Course> GetFilteredCoursesByTheme(Theme theme)
        //{
        //    var c = _themeRepository.GetFilteredCourses((x => x.theme == theme))
        //    var courses = _themeRepository.GetFilteredEntities<Course>((x => x.theme == theme));
        //    return courses;
        //}
    }
}
