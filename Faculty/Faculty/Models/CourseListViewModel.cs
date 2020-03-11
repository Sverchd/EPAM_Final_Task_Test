using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Faculty.Models
{
    public class CourseListViewModel
    {
        public List<CourseView> Courses { get; set; }
        public string sortParam { get; set; }
        public bool sortInverse { get; set; }
        public CourseListViewModel()
        {
            Courses = new List<CourseView>();
        }

        

    }
}