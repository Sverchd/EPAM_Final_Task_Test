using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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