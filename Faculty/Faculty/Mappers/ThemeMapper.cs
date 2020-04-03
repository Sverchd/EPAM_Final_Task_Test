using BusinessLogicLayer.Models;
using Faculty.Models;

namespace Faculty.Mappers
{
    /// <summary>
    ///     ThemeView to theme mapper class
    /// </summary>
    public static class ThemeMapper
    {
        /// <summary>
        ///     Map method
        /// </summary>
        /// <param name="themeView">themeView instance</param>
        /// <returns>instance of Theme (for BLL)</returns>
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

    /// <summary>
    ///     Theme to ThemeView mapper class
    /// </summary>
    public static class ThemeViewMapper
    {
        /// <summary>
        ///     map method
        /// </summary>
        /// <param name="theme">theme instance</param>
        /// <param name="count">count of courses</param>
        /// <returns></returns>
        public static ThemeView Map(this Theme theme, int count = 0)
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