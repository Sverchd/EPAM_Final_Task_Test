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
        public int? Mark { get; set; }

        public GradeViewModel()
        {

        }

        public GradeViewModel(string student, int? mark)
        {
            Student = student;
            Mark = mark;
        }
    }
}