using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IThemeRepository
    {
        List<Theme> GetAllThemes();

        bool AddTheme(Theme theme);

        bool DeleteTheme(int themeId);
        //List<T> GetFilteredEntities<T>(Func<T, bool> predicate) where T : class;
        //List<Course> GetFilteredCourses(Func<ICourse, bool> predicate);
    }

}
