using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogicLayer;
using DataAccessLayer.Context;
using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public class CourseRepository : ICourseRepository
    {
        private FacultyDbContext _facultyDbContext;

        public CourseRepository(FacultyDbContext facultyDbContext)
        {
            _facultyDbContext = facultyDbContext;
        }
        public List<Course> GetAllCourses()
        {
            
                var datalist = _facultyDbContext.Courses.Include("theme").ToList();
                var courses = new List<Course>() { };
                
                foreach (var courseEntity in datalist)
                {
                    courses.Add(new Course(courseEntity.CourseEntityId,new Theme(courseEntity.theme.ThemeEntityId,courseEntity.theme.Name ), courseEntity.name, courseEntity.start, courseEntity.end));
                    
                }
                return courses;
        }

        public List<Course> GetCoursesByTheme(Theme theme)
        {
            var entityTheme = new ThemeEntity(theme.Name);
            var Entitycourses = _facultyDbContext.Courses.Include("theme").Where(x => x.theme.Name == entityTheme.Name).ToList();
            var courses = new List<Course>() { };
            if(Entitycourses.Count>0)
            foreach (var ecourse in Entitycourses)
            {
                courses.Add(new Course(ecourse.CourseEntityId, new Theme(ecourse.theme.ThemeEntityId,ecourse.theme.Name), ecourse.name,ecourse.start,ecourse.end ));
            }

            return courses;
        }
    }
}
