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
        public Theme theme { get; set; }

        /// <summary>
        ///     name of the course
        /// </summary>
        public string name { get; set; }

        /// <summary>
        ///     start time of the course
        /// </summary>
        public DateTime start { get; set; }

        /// <summary>
        ///     end time of course
        /// </summary>
        public DateTime end { get; set; }

        /// <summary>
        ///     list of student of the course
        /// </summary>
        public List<User> students;

        /// <summary>
        ///     teacher of the course
        /// </summary>
        public User teacher;

        /// <summary>
        ///     length of the course (days)
        /// </summary>
        public int length { get; set; }

        /// <summary>
        ///     constructor of the course
        /// </summary>
        public Course()
        {
            teacher = new User();
            students = new List<User>();
        }

        /// <summary>
        ///     constructor of the course
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="th">theme</param>
        /// <param name="nm">name</param>
        /// <param name="sdt">start date</param>
        /// <param name="edt">end date</param>
        public Course(int id, Theme th, string nm, DateTime sdt, DateTime edt)
        {
            CourseId = id;
            theme = th;
            name = nm;
            start = sdt;
            end = edt;
        }

        /// <summary>
        ///     constructor of the course
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="th">theme</param>
        /// <param name="nm">name</param>
        /// <param name="sdt">start date</param>
        /// <param name="edt">end date</param>
        /// <param name="tch">teacher</param>
        public Course(int id, Theme th, string nm, DateTime sdt, DateTime edt, User tch)
        {
            CourseId = id;
            theme = th;
            name = nm;
            start = sdt;
            end = edt;
            teacher = tch;
        }
    }
}