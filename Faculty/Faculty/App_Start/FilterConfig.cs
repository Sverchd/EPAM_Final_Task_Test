using System.Web.Mvc;

namespace Faculty
{
    public class FilterConfig
    {
        /// <summary>
        ///     Registers global filters
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}