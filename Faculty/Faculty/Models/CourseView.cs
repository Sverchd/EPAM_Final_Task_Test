using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Faculty.Models
{
    public class CourseView
    {
        public int CourseEntityId { get; set; }
        [Display(Name = "Theme")] public ThemeView Theme { get; set; }
        [Display(Name = "Name")] public string Name { get; set; }

        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; }

        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime End { get; set; }

        [Display(Name = "Length, days")] public int Length { get; set; }
        public int studentsCount { get; set; }
        public List<UserView> Students { get; set; }
        [Display(Name = "Teacher")] public UserView teacher { get; set; }

        public CourseView(int id, ThemeView th, string nm, DateTime sdt, DateTime edt)
        {
            CourseEntityId = id;
            Theme = th;
            Name = nm;
            Start = sdt;
            End = edt;
        }

        public CourseView()
        {
        }
    }
}