namespace BusinessLogicLayer.Models
{
    public class Theme
    {
        public int ThemeId { get; set; }
        public string Name { get; set; }

        public Theme()
        {
        }

        public int CourseCount { get; set; }

        public Theme(int id, string nm)
        {
            ThemeId = id;
            Name = nm;
        }
    }
}