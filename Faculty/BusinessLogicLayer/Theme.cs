using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class Theme
    {
        public int ThemeId { get; set; }
        public string Name { get; set; }
        public Theme()
        { }
        public int CourseCount { get; set; }
        public Theme(int id,string nm)
        {
            ThemeId = id;
            Name = nm;
        }
    }
}
