using System.Collections.Generic;

namespace Faculty.Models
{
    public class CourseListViewModel
    {
        public List<CourseView> Courses { get; set; }
        public UserView user { get; set; }

        public CourseListViewModel()
        {
            Courses = new List<CourseView>();
        }
    }
}