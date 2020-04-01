using System.Collections.Generic;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Contracts
{
    public interface IThemeService
    {
        List<Theme> GetAllThemes();

        bool AddTheme(Theme theme);
        //List<Course> GetFilteredCoursesByTheme(Theme theme);
        bool DeleteTheme(int id);
        bool Edit(Theme theme);
        Theme GeThemeById(int id);
    }
}
