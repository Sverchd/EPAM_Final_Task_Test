using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public Theme theme { get; set; }
        public string name { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public Dictionary<string, int?> gradebook;
        public List<User> students;
        public User teacher;
        public int length { get; set; }
        public Course()
        {
            teacher = new User();
            students = new List<User>();
        }

        //public Course(Theme th, string nm, DateTime sdt, DateTime edt)
        //{
        //    theme = th;
        //    name = nm;
        //    start = sdt;
        //    end = edt;
        //}

        public Course(int id, Theme th, string nm, DateTime sdt, DateTime edt)
        {
            CourseId = id;
            theme = th;
            name = nm;
            start = sdt;
            end = edt;
            //students = std;
            //teacher = tch;
        }
        public Course(int id, Theme th, string nm, DateTime sdt, DateTime edt, User tch)
        {
            CourseId = id;
            theme = th;
            name = nm;
            start = sdt;
            end = edt; 
            //students = std;
            teacher = tch;
        }
    }
}
