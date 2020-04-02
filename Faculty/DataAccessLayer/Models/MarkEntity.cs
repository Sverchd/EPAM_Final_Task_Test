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

        public MarkEntity()
        {

        }

        public MarkEntity(CourseEntity course, AppUser student, int? mark)
        {
            Course = course;
            Student = student;
            Mark = mark;
        }
    }
}
