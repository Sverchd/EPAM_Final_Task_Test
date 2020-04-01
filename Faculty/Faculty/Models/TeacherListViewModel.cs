using System.Collections.Generic;

namespace Faculty.Models
{
    public class TeacherListViewModel
    {
        public List<UserView> Teachers { get; set; }

        public TeacherListViewModel()
        {
            Teachers = new List<UserView>();
        }
    }
}