using System.Collections.Generic;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Contracts
{
    public interface IThemeRepository
    {
        List<Theme> GetAllThemes();

        Theme AddTheme(Theme theme);

        bool DeleteTheme(int themeId);

        Theme Edit(Theme theme);
    }
}