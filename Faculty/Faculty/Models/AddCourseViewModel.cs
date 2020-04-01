using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Faculty.Models
{
    public class AddCourseViewModel
    {
        public int CourseId { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(75)]
        public string name { get; set; }

        [Required] [Display(Name = "Theme")] public int theme { get; set; }
        public IEnumerable<SelectListItem> Themes { get; set; }
        [Required] [Display(Name = "Teacher")] public string Teacher { get; set; }
        public IEnumerable<SelectListItem> Teachers { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start date")]
        public DateTime start { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "End date")]
        public DateTime end { get; set; }
    }
}