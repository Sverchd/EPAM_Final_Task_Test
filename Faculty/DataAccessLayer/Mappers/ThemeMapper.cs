using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Models;
using DataAccessLayer.Models;

namespace DataAccessLayer.Mappers
{
    public static class ThemeMapper
    {
        public static Theme Map(this ThemeEntity themeEntity)
        {
            var resultTheme = new Theme
            {
                ThemeId = themeEntity.ThemeEntityId,
                Name = themeEntity.Name
            };
            return resultTheme;
        }
    }


    public static class ThemeEntityMapper
    {
        public static ThemeEntity Map(this Theme themeEntity)
        {
            var resultTheme = new ThemeEntity
            {
                ThemeEntityId = themeEntity.ThemeId,
                Name = themeEntity.Name
            };
            return resultTheme;
        }
    }
}
