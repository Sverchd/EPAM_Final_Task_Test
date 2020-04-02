using BusinessLogicLayer.Models;
using DataAccessLayer.Models;

namespace DataAccessLayer.Mappers
{
    /// <summary>
    ///     ThemeEntity to Theme mapper class
    /// </summary>
    public static class ThemeMapper
    {
        /// <summary>
        ///     map method
        /// </summary>
        /// <param name="themeEntity">ThemeEntity instance</param>
        /// <returns>instance of Theme (for BLL)</returns>
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

    /// <summary>
    ///     Theme to ThemeEntity mapper class
    /// </summary>
    public static class ThemeEntityMapper
    {
        /// <summary>
        ///     map method
        /// </summary>
        /// <param name="theme">Theme instance </param>
        /// <returns>instance of ThemeEntity (for DAL)</returns>
        public static ThemeEntity Map(this Theme theme)
        {
            var resultTheme = new ThemeEntity
            {
                ThemeEntityId = theme.ThemeId,
                Name = theme.Name
            };
            return resultTheme;
        }
    }
}