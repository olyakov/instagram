using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instagram.Data.Model;

namespace Instagram.Services
{
    interface IUserService
    {
        AspNetUsers GetUserById(string id);
        Task<AspNetUsers> GetCurrentUser();
        //Task GetUserByUserName(string userName);
    }
}
