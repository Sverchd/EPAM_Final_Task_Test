using System.Collections.Generic;

namespace BusinessLogicLayer.Models
{
    public class User
    {
        public string id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public List<Course> Courses { get; set; }

        /// <summary>
        ///     default user constructor
        /// </summary>
        public User()
        {
            Courses = new List<Course>();
        }

        /// <summary>
        ///     user constructor with parameters
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="role">role</param>
        /// <param name="courses">list of courses</param>
        public User(string email, string role, List<Course> courses)
        {
            Name = email;
            Email = email;
            Role = role;
            Courses = courses;
        }
    }
}