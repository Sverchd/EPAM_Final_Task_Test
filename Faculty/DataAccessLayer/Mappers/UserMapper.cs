using System.Linq;
using BusinessLogicLayer.Models;
using DataAccessLayer.Models;

namespace DataAccessLayer.Mappers
{
    /// <summary>
    ///     AppUser to User mapper class
    /// </summary>
    public static class UserMapper
    {
        /// <summary>
        ///     Map method
        /// </summary>
        /// <param name="userEntity">AppUser instance</param>
        /// <param name="role">role of the user</param>
        /// <returns>User instance (for BLL)</returns>
        public static User Map(this AppUser userEntity, string role = "")
        {
            var resultUser = userEntity.MapFlat(role);
            if (userEntity.Courses == null)
                resultUser.Courses = userEntity.Scourses?.Select(x => x.MapFlat()).ToList();
            else
                resultUser.Courses = userEntity.Courses?.Select(x => x.MapFlat()).ToList();
            return resultUser;
        }

        /// <summary>
        ///     method for flat mapping (without complex properties)
        /// </summary>
        /// <param name="userEntity">AppUser instance</param>
        /// <param name="role">role of the user</param>
        /// <returns>User instance (for BLL)</returns>
        public static User MapFlat(this AppUser userEntity, string role = "")
        {
            var resultUser = new User();
            resultUser.Id = userEntity.Id;
            resultUser.Name = userEntity.UserName;
            resultUser.Email = userEntity.Email;
            resultUser.Role = role;
            return resultUser;
        }
    }

    /// <summary>
    ///     User to AppUser mapper class
    /// </summary>
    public static class UserEntityMapper
    {
        /// <summary>
        ///     map method
        /// </summary>
        /// <param name="User">User instance</param>
        /// <returns>AppUser instance (for DAL)</returns>
        public static AppUser Map(this User User)
        {
            var resultUser = User.MapFlat();
            resultUser.Courses = User.Courses?.Select(x => x.MapFlat()).ToList();
            return resultUser;
        }

        /// <summary>
        ///     method for flat mapping (without complex properties)
        /// </summary>
        /// <param name="User">User instance</param>
        /// <returns>AppUser instance (for DAL)</returns>
        public static AppUser MapFlat(this User User)
        {
            var resultUser = new AppUser();
            resultUser.Id = User.Id;
            resultUser.UserName = User.Name;
            resultUser.Email = User.Email;
            return resultUser;
        }
    }
}