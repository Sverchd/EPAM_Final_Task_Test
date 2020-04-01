using System.ComponentModel.DataAnnotations;

namespace Faculty.Models
{
    public class ThemeView
    {
        public int ThemeEntityId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Course count")]
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
        public ThemeView(int id, string nm)
        {
            ThemeEntityId = id;
            Name = nm;
            
        }
    }
}