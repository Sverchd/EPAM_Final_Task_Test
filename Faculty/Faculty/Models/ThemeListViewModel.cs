using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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