namespace BusinessLogicLayer.Models
{
    public class Theme
    {
        public int ThemeId { get; set; }
        public string Name { get; set; }
        public int CourseCount { get; set; }

        /// <summary>
        ///     default theme constructor
        /// </summary>
        public Theme()
        {
        }

        /// <summary>
        ///     theme constructor
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="nm">name</param>
        public Theme(int id, string nm)
        {
            ThemeId = id;
            Name = nm;
        }
    }
}