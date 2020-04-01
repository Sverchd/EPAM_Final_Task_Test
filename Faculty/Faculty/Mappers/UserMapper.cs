using System.Linq;
using BusinessLogicLayer.Models;
using Faculty.Models;

namespace Faculty.Mappers
{
    public static class UserMapper
    {
        public static User Map(this UserView userView)
        {
            var resultUser = userView.MapFlat();
            resultUser.Courses = userView.Courses.Select(x => x.MapFlat()).ToList();
            return resultUser;
        }

        public static User MapFlat(this UserView userView)
        {
            var resultUser = new User();
            resultUser.Name = userView.Name;
            resultUser.Email = userView.Email;


            return resultUser;
        }
    }

    public static class UserViewMapper
    {
        public static UserView Map(this User user)
        {
            var resultUser = user.MapFlat();
            resultUser.Courses.Select(x => x.MapFlat());
            resultUser.CourseCount = user.Courses.Count;
            return resultUser;
        }

        public static UserView MapFlat(this User user)
        {
            var resultUser = new UserView();
            resultUser.Name = user.Name;
            resultUser.Email = user.Email;


            return resultUser;
        }
    }
}