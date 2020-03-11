using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogicLayer;

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
