using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Faculty.Models
{
    public class GradebookViewModel
    {
        public IList<GradeViewModel> Grades;
        public int courseId;

        public GradebookViewModel()
        {
            Grades = new List<GradeViewModel>();
        }

        public GradebookViewModel(IList<GradeViewModel> grades)
        {
            Grades = grades;
        }
    }
    
}