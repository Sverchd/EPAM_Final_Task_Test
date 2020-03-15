using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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