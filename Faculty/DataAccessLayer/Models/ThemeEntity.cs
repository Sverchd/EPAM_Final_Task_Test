namespace DataAccessLayer.Models
{
    public class ThemeEntity
    {
        public int ThemeEntityId { get; set; }
        public string Name { get; set; }
        public ThemeEntity()
        { }

        
        public ThemeEntity(string nm)
        {
            Name = nm;
        }
    }
}
