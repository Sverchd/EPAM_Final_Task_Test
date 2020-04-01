using System.Collections.Generic;
using Microsoft.Ajax.Utilities;

namespace Faculty.Models
{
    public class CourseListViewModel
    {
        public List<CourseView> Courses { get; set; }
        public UserView user { get; set; }
        public bool sortInverse { get; set; }
        public CourseListViewModel()
        {
            Courses = new List<CourseView>();
        }

        

    }
}