using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Faculty.Models
{
    public class GradeViewModel
    {
        [Display(Name = "Name")]
        public string Student { get; set; }
        [Required]
        [Display(Name = "Grade")]
        [Range(1, 100)]
        public int? Mark { get; set; }
        public int CourseId { get; set; }

        public GradeViewModel()
        {

        }

        public GradeViewModel(string student, int courseId, int? mark)
        {
            Student = student;
            CourseId = courseId;
            Mark = mark;
        }
    }
}