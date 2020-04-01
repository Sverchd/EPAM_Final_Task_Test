using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Faculty.Models
{
    public class UserView
    {
        public string Name { get; set; }
        [EmailAddress] public string Email { get; set; }
        public string Role { get; set; }
        [PasswordPropertyText] public string Password { get; set; }
        public List<CourseView> Courses { get; set; }
        [Display(Name = "Course count")] public int CourseCount { get; set; }

        public UserView()
        {
            Courses = new List<CourseView>();
        }

        public UserView(string name, string role)
        {
            Name = name;
            Role = role;
            Email = name;
        }

        public UserView(string name, string role, int count)
        {
            Name = name;
            Role = role;
            CourseCount = count;
            Email = name;
        }

        public UserView(string name, string role, List<CourseView> courses)
        {
            Name = name;
            Role = role;
            Courses = courses;
            Email = name;
        }
    }
}