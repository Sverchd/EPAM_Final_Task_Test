using System.Collections.Generic;

namespace Faculty.Models
{
    public class StudentListViewModel
    {
        public List<UserView> Students { get; set; }

        public StudentListViewModel()
        {
            Students = new List<UserView>();
        }
    }
}