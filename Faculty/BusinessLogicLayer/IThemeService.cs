using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IThemeService
    {
        List<Theme> GetAllThemes();

        bool AddTheme(Theme theme);
        //List<Course> GetFilteredCoursesByTheme(Theme theme);
        bool DeleteTheme(int id);
    }
}
