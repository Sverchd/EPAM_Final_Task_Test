using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class Mark
    {
        public int MarkId { get; set; }
        public Course Course { get; set; }
        public User Student { get; set; }
        public int? Grade { get; set; }

        public Mark()
        {

        }

        public Mark(Course course, User student, int? grade)
        {
            Course = course;
            Student = student;
            Grade = grade;
        }
    }
}
