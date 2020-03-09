using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Instagram.Data.Model;

namespace Instagram.Services
{
    public interface IUser
    {
        AspNetUsers GetUserById(string id);
        AspNetUsers GetUserByUsername(string username);
        Task<AspNetUsers> GetCurrentUser(ClaimsPrincipal claim);
    }
}
