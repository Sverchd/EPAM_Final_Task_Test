using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Models;

namespace DataAccessLayer.Models
{
    public class MarkEntity
    {
        public int MarkEntityId { get; set; }
        public CourseEntity Course { get; set; }
        public AppUser Student { get; set; }
        public int? Mark { get; set; }
        /// <summary>
        /// default constructor
        /// </summary>
        public MarkEntity()
        {

        }
        /// <summary>
        /// constructor with parameters
        /// </summary>
        /// <param name="course">course</param>
        /// <param name="student">student</param>
        /// <param name="mark">mark</param>
        public MarkEntity(CourseEntity course, AppUser student, int? mark)
        {
            Course = course;
            Student = student;
            Mark = mark;
        }
    }
}
