using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Models
{
    public class Course
    {
        /// <summary>
        ///     id of the course
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        ///     theme of the course
        /// </summary>
        public Theme Theme { get; set; }

        /// <summary>
        ///     name of the course
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     start time of the course
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        ///     end time of course
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        ///     list of student of the course
        /// </summary>
        public List<User> Students;

        /// <summary>
        ///     teacher of the course
        /// </summary>
        public User Teacher;

        /// <summary>
        ///     length of the course (days)
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        ///     constructor of the course
        /// </summary>
        public Course()
        {
            Teacher = new User();
            Students = new List<User>();
        }

        /// <summary>
        ///     constructor of the course
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="theme">theme</param>
        /// <param name="name">name</param>
        /// <param name="start">start date</param>
        /// <param name="end">end date</param>
        public Course(int id, Theme theme, string name, DateTime start, DateTime end)
        {
            CourseId = id;
            Theme = theme;
            Name = name;
            Start = start;
            End = end;
            Teacher = new User();
            Students = new List<User>();
        }

        /// <summary>
        ///     constructor of the course
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="theme">theme</param>
        /// <param name="name">name</param>
        /// <param name="start">start date</param>
        /// <param name="end">end date</param>
        /// <param name="teacher">teacher</param>
        public Course(int id, Theme theme, string name, DateTime start, DateTime end, User teacher)
        {
            CourseId = id;
            Theme = theme;
            Name = name;
            Start = start;
            End = end;
            Teacher = teacher;
            
            Students = new List<User>();
        }
    }
}