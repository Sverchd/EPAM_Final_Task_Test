using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Models;
using DataAccessLayer.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer.Mappers
{
    public static class UserMapper
    {
        public static User Map(this AppUser UserEntity, string role="")
        {
            var resultUser = UserEntity.MapFlat(role);
            if (UserEntity.courses == null)
            {
                resultUser.Courses= UserEntity.scourses?.Select(x => x.MapFlat()).ToList();
            }
            else
            resultUser.Courses = UserEntity.courses?.Select(x => x.MapFlat()).ToList();
            return resultUser;
        }
        public static User MapFlat(this AppUser UserEntity, string role="")
        {
            var resultUser = new User();
            resultUser.id = UserEntity.Id;
            resultUser.Name = UserEntity.UserName;
            resultUser.Email = UserEntity.Email;
            resultUser.Role = role;
            
            return resultUser;
        }
    }
    public static class UserEntityMapper
    {
        public static AppUser Map(this User User)
        {
            var resultUser = User.MapFlat();
            resultUser.courses = User.Courses?.Select(x => x.MapFlat()).ToList();
            return resultUser;
        }
        public static AppUser MapFlat(this User User)
        {
            var resultUser = new AppUser();
            resultUser.Id = User.id;
            resultUser.UserName = User.Name;
            resultUser.Email = User.Email;
            

            return resultUser;
        }
    }
}
