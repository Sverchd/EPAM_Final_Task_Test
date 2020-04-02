using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer.Models
{
    public class AppUser : IdentityUser
    {
        /// <summary>
        /// list of courses for teachers
        /// </summary>
        public List<CourseEntity> courses { get; set; }
        /// <summary>
        /// list of courses for students
        /// </summary>
        public List<CourseEntity> scourses { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}