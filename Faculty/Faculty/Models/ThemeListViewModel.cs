using System.Collections.Generic;

namespace Faculty.Models
{
    public class ThemeListViewModel
    {
        public List<ThemeView> Themes { get; set; }
        public ThemeListViewModel()
        {
            Themes = new List<ThemeView>();
        }

    }
}