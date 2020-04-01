using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Faculty.Models
{
    public class CourseView
    {
        public int CourseEntityId { get; set; }
        [Display(Name = "Theme")] public ThemeView theme { get; set; }
        [Display(Name = "Name")] public string name { get; set; }

        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime start { get; set; }

        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime end { get; set; }

        [Display(Name = "Length, days")] public int length { get; set; }
        public int studentsCount { get; set; }
        public Dictionary<string, int?> gradebook;
        public List<UserView> students { get; set; }
        [Display(Name = "Teacher")] public UserView teacher { get; set; }

        public CourseView(int id, ThemeView th, string nm, DateTime sdt, DateTime edt)
        {
            CourseEntityId = id;
            theme = th;
            name = nm;
            start = sdt;
            end = edt;
        }

        public CourseView()
        {
        }
    }
}