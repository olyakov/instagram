using Instagram.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Services
{
    public class ActionWithUser
    {
        //var userName = User.FindFirst(ClaimTypes.Name).Value;
        public static async Task<AspNetUsers> GetCurrentUserAsync(IHttpContextAccessor httpContextAccessor, UserManager<AspNetUsers> userManager)
        {
            var httpcontext = httpContextAccessor.HttpContext;
            var user = (await userManager.GetUserAsync(httpcontext.User));
            return user;
        }
    }
}
