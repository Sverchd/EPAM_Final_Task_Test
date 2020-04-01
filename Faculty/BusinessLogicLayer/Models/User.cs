using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace BusinessLogicLayer.Models
{
    public class User
    {
        public string id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public List<Course> Courses { get; set; }

        public User()
        {
            Courses=new List<Course>();
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
