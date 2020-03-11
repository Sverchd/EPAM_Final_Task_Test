using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.AspNet.Identity;

namespace DataAccessLayer.Interfaces
{
    public interface IUserService
    {
        IdentityResult Create(AppUser user, string password);

        IdentityResult AddToRole(string userId, string role);

        bool IsInRole(string userId, string role);
        IdentityResult AddLogin(string id, UserLoginInfo login);
    }
}
