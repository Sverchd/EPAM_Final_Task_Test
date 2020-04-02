using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class CourseEntity
    {

        public int CourseEntityId { get; set; }
        public ThemeEntity theme { get; set; }
        public string name { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        [InverseProperty("scourses")] public List<AppUser> students { get; set; }
        [InverseProperty("courses")] public AppUser Teacher { get; set; }
        public Dictionary<string, int?> gradebook { get; set; }
        /// <summary>
        /// course constructor
        /// </summary>
        public CourseEntity()
        {
            students = new List<AppUser>();
        }
        /// <summary>
        /// course constructor with parameters
        /// </summary>
        /// <param name="th">theme</param>
        /// <param name="nm">name</param>
        /// <param name="sdt">start time</param>
        /// <param name="edt">end time</param>
        /// <param name="teacher">teacher</param>
        public CourseEntity(ThemeEntity th, string nm, DateTime sdt, DateTime edt, AppUser teacher)
        {
            theme = th;
            name = nm;
            start = sdt;
            end = edt;
            Teacher = teacher;
            students = new List<AppUser>();
        }
        /// <summary>
        /// course constructor with parameters
        /// </summary>
        /// <param name="th">theme</param>
        /// <param name="nm">name</param>
        /// <param name="sdt">start time</param>
        /// <param name="edt">end time</param>
        public CourseEntity(ThemeEntity th, string nm, DateTime sdt, DateTime edt)
        {
            theme = th;
            name = nm;
            start = sdt;
            end = edt;
            students = new List<AppUser>();
        }
    }
}