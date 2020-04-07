using System.Collections.Generic;

namespace Faculty.Models
{
    public class StudentListViewModel
    {
        public List<UserView> Students { get; set; }
        public List<UserView> Banned { get; set; }

        public StudentListViewModel()
        {
            Students = new List<UserView>();
            Banned = new List<UserView>();
        }
    }
}