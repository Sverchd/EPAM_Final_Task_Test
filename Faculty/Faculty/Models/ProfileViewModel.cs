using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Faculty.Models
{
    public class ProfileViewModel
    {
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Role")] public string role { get; set; }
        public List<CourseView> Courses;
        public List<CourseView> CoursesApplied;
        public List<CourseView> CoursesInProgress;
        public List<CourseView> CoursesFinished;
        public ProfileViewModel()
        {
            Courses = new List<CourseView>();
            CoursesApplied = new List<CourseView>();
            CoursesFinished = new List<CourseView>();
            CoursesInProgress = new List<CourseView>();
        }
    }
}