using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Faculty.Models
{
    public class CourseView
    {
        public int CourseEntityId { get; set; }
        public ThemeView theme { get; set; }
        public string name { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }

        public CourseView(int id, ThemeView th, string nm, DateTime sdt, DateTime edt)
        {
            CourseEntityId = id;
            theme = th;
            name = nm;
            start = sdt;
            end = edt;
            //students = std;
            //teacher = tch;
        }
    }
}