using Instagram.Data;
using Instagram.Data.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Instagram.Services
{
    public class UserService : IUser
    {
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly IdentityDbContext _userCtx;

        public UserService(UserManager<AspNetUsers> userManager, IdentityDbContext userCtx)
        {
            _userManager = userManager;
            _userCtx = userCtx;
        }

        public Task<AspNetUsers> GetCurrentUser(ClaimsPrincipal claim) => _userManager.GetUserAsync(claim);

        public AspNetUsers GetUserById(string id) => _userCtx.ApplicationUsers.FirstOrDefault(u => u.Id == id);

        public AspNetUsers GetUserByUsername(string username) => _userCtx.ApplicationUsers.FirstOrDefault(u => u.UserName == username);
    }
}
