using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogicLayer.Models;
using Faculty.Models;


namespace Faculty.Mappers
{
    public static class ThemeMapper
    {
        public static Theme Map(this ThemeView themeView)
        {
            var resultTheme = new Theme
            {
                ThemeId = themeView.ThemeEntityId,
                Name = themeView.Name
            };
            return resultTheme;
        }
    }


    public static class ThemeViewMapper
    {
        public static ThemeView Map(this Theme theme, int count=0)
        {
            var resultTheme = new ThemeView
            {
                ThemeEntityId = theme.ThemeId,
                Name = theme.Name,
                CourseCount = count
            };
            return resultTheme;
        }
    }
}