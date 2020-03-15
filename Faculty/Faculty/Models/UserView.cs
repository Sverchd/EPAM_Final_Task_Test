using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogicLayer;

namespace Faculty.Models
{
    public class UserView
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public List<CourseView> Courses { get; set; }
        public int CourseCount { get; set; }
        public UserView()
        {

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