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
        /// <param name="nm">name of theme</param>
        public ThemeEntity(string nm)
        {
            Name = nm;
        }
    }
}