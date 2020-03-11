using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;

namespace DataAccessLayer.Models
{
    public class CourseEntity
    {
        public int CourseEntityId { get; set; }
        public ThemeEntity theme { get; set; }
        public string name { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        private List<AppUser> students;
        public AppUser teacher;
        
        public CourseEntity()
        {

        }

        //public Course(Theme th, string nm, DateTime sdt, DateTime edt)
        //{
        //    theme = th;
        //    name = nm;
        //    start = sdt;
        //    end = edt;
        //}
        public CourseEntity(ThemeEntity th, string nm, DateTime sdt, DateTime edt)
        {
            theme = th;
            name = nm;
            start = sdt;
            end = edt;
            //students = std;
            //teacher = tch;
        }
    }
}
