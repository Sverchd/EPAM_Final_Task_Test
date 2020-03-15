using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class User
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public List<Course> Courses { get; set; }

        public User()
        {

        }

        public User(string email, string role, List<Course> courses)
        {
            Name = email;
            Email = email;
            Role = role;
            Courses = courses;
        }
    }
}
