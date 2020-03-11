using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class Course : ICourse
    {
        public int CourseId { get; set; }
        public Theme theme { get; set; }
        public string name { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        //private List<ApplicationUser> students;
        //public ApplicationUser teacher;
        public Course()
        {

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
    }
}
