using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Faculty.Models
{
    public class ThemeView
    {
        public int ThemeEntityId { get; set; }
        [Required]
        public string Name { get; set; }
        public int CourseCount { get; set; }
        public ThemeView()
        { }

        public ThemeView(string nm)
        {
            Name = nm;
        }
        public ThemeView(int id, string nm, int courseCount)
        {
            ThemeEntityId = id;
            Name = nm;
            CourseCount = courseCount;
        }
    }
}