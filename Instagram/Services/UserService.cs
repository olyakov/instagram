using Instagram.Data;
using Instagram.Data.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public AspNetUsers GetCurrentUser(ClaimsPrincipal claim) => _userCtx.ApplicationUsers
            .Include(u => u.Followers)
            .ThenInclude(f => f.Follower)
            .Include(u => u.Followings)
            .ThenInclude(f => f.Following)
            .FirstOrDefault(u => u.UserName == claim.Identity.Name);

        public AspNetUsers GetUserById(string id) => _userCtx.ApplicationUsers.FirstOrDefault(u => u.Id == id);

        public AspNetUsers GetUserByUsername(string username) => _userCtx.ApplicationUsers
                .Include(u => u.Followers)
                .ThenInclude(f => f.Follower)
                .Include(u => u.Followings)
                .ThenInclude(f => f.Following)
                .FirstOrDefault(u => u.UserName == username);
    }
}
