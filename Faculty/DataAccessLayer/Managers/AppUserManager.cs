﻿using DataAccessLayer.Context;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace DataAccessLayer.Managers
{
    public class AppUserManager : UserManager<AppUser>, IUserService
    {
        public AppUserManager(IUserStore<AppUser> store)
            : base(store)
        {
        }

        public static AppUserManager Create(
            IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var manager = new AppUserManager(
                new UserStore<AppUser>(context.Get<FacultyDbContext>()));
            manager.UserValidator = new UserValidator<AppUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            return manager;
        }

        public IdentityResult AddToRole(string userId, string role)
        {
            return AddToRoleAsync(userId, role).Result;
        }
        public IdentityResult RemoveFromRole(string userId, string role)
        {
            return RemoveFromRoleAsync(userId, role).Result;
        }
        public IdentityResult Create(AppUser user, string password)
        {
            return CreateAsync(user, password).Result;
        }

        public bool IsInRole(string userId, string role)
        {
            return IsInRoleAsync(userId, role).Result;
        }

        public IdentityResult AddLogin(string userid, UserLoginInfo login)
        {
            return AddLoginAsync(userid, login).Result;
        }
    }
}