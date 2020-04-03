using System.Linq;
using BusinessLogicLayer.Models;
using Faculty.Models;

namespace Faculty.Mappers
{
    /// <summary>
    ///     UserView to User mapper class
    /// </summary>
    public static class UserMapper
    {
        /// <summary>
        ///     Map method
        /// </summary>
        /// <param name="userView">UserView instance</param>
        /// <returns>instance of User (for BLL)</returns>
        public static User Map(this UserView userView)
        {
            var resultUser = userView.MapFlat();
            resultUser.Courses = userView.Courses.Select(x => x.MapFlat()).ToList();
            return resultUser;
        }

        /// <summary>
        ///     method for flat mapping (without complex properties)
        /// </summary>
        /// <param name="userView">UserView instance</param>
        /// <returns>instance of User (for BLL)</returns>
        public static User MapFlat(this UserView userView)
        {
            var resultUser = new User();
            resultUser.Name = userView.Name;
            resultUser.Email = userView.Email;
            return resultUser;
        }
    }

    /// <summary>
    ///     User to UserView mapper class
    /// </summary>
    public static class UserViewMapper
    {
        /// <summary>
        ///     Map method
        /// </summary>
        /// <param name="user">User instance</param>
        /// <returns>instance of UserView (for Presentation)</returns>
        public static UserView Map(this User user)
        {
            var resultUser = user.MapFlat();
            resultUser.Courses.Select(x => x.MapFlat());
            resultUser.CourseCount = user.Courses.Count;
            return resultUser;
        }

        /// <summary>
        ///     method for flat mapping (without complex properties)
        /// </summary>
        /// <param name="user">User instance</param>
        /// <returns>instance of UserView (for Presentation)</returns>
        public static UserView MapFlat(this User user)
        {
            var resultUser = new UserView();
            resultUser.Name = user.Name;
            resultUser.Email = user.Email;
            return resultUser;
        }
    }
}