using System.Collections.Generic;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Contracts
{
    public interface IThemeRepository
    {
        List<Theme> GetAllThemes();

        bool AddTheme(Theme theme);

        bool DeleteTheme(int themeId);

        bool Edit(Theme theme);
        //List<T> GetFilteredEntities<T>(Func<T, bool> predicate) where T : class;
        //List<Course> GetFilteredCourses(Func<ICourse, bool> predicate);
    }

}
