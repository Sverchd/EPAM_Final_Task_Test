using System.Collections.Generic;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Contracts
{
    public interface IThemeService
    {
        List<Theme> GetAllThemes();

        Theme AddTheme(Theme theme);

        bool DeleteTheme(int id);
        Theme Edit(Theme theme);
        Theme GetThemeById(int id);
        Theme GetThemeByName(string themeName);
    }
}