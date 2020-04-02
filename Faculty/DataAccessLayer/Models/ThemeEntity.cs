namespace DataAccessLayer.Models
{
    public class ThemeEntity
    {
        public int ThemeEntityId { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// default constructor
        /// </summary>
        public ThemeEntity()
        {
        }

        /// <summary>
        /// constructor with parameter
        /// </summary>
        /// <param name="name">name of Theme</param>
        public ThemeEntity(string name)
        {
            Name = name;
        }
    }
}