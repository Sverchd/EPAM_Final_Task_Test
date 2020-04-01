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
    }
}