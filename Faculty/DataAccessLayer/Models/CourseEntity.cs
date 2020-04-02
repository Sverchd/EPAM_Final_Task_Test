using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class CourseEntity
    {

        public int CourseEntityId { get; set; }
        public ThemeEntity Theme { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        [InverseProperty("scourses")] public List<AppUser> Students { get; set; }
        [InverseProperty("courses")] public AppUser Teacher { get; set; }
        
        /// <summary>
        /// course constructor
        /// </summary>
        public CourseEntity()
        {
            Students = new List<AppUser>();
        }
        /// <summary>
        /// course constructor with parameters
        /// </summary>
        /// <param name="theme">Theme</param>
        /// <param name="name">name</param>
        /// <param name="start">start time</param>
        /// <param name="end">end time</param>
        /// <param name="teacher">teacher</param>
        public CourseEntity(ThemeEntity theme, string name, DateTime start, DateTime end, AppUser teacher)
        {
            Theme = theme;
            Name = name;
            Start = start;
            End = end;
            Teacher = teacher;
            Students = new List<AppUser>();
        }
        /// <summary>
        /// course constructor with parameters
        /// </summary>
        /// <param name="theme">Theme</param>
        /// <param name="name">name</param>
        /// <param name="start">start time</param>
        /// <param name="end">end time</param>
        public CourseEntity(ThemeEntity theme, string name, DateTime start, DateTime end)
        {
            Theme = theme;
            Name = name;
            Start = start;
            End = end;
            Students = new List<AppUser>();
        }
    }
}