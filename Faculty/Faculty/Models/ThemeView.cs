using System.ComponentModel.DataAnnotations;

namespace Faculty.Models
{
    public class ThemeView
    {
        public int ThemeEntityId { get; set; }
        [Required(ErrorMessage = "Please enter name"), MaxLength(30)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Course count")] public int CourseCount { get; set; }

        public ThemeView()
        {
        }

        public ThemeView(string name)
        {
            Name = name;
        }

        public ThemeView(int id, string name, int courseCount)
        {
            ThemeEntityId = id;
            Name = name;
            CourseCount = courseCount;
        }

        public ThemeView(int id, string name)
        {
            ThemeEntityId = id;
            Name = name;
        }
    }
}