using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Faculty.Models
{
    public class ProfileViewModel
    {
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Role")]
        public string role { get; set; }

        public List<CourseView> Courses;

        public ProfileViewModel()
        {
            Courses = new List<CourseView>();
        }
    }
}