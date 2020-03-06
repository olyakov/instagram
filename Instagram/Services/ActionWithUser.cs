using Instagram.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Services
{
    public class ActionWithUser
    {

        public static async Task<string> GetCurrentUserAsync(IHttpContextAccessor httpContextAccessor, UserManager<AspNetUsers> userManager)
        {
            var httpcontext = httpContextAccessor.HttpContext;
            var userId = (await userManager.GetUserAsync(httpcontext.User)).Id;
            return userId;
        }
    }
}
