using Instagram.Data;
using Instagram.Data.Model;
using Instagram.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

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
