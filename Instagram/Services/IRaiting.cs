using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instagram.Data.Model;

namespace Instagram.Services
{
    public interface IRaiting
    {
        Task SetLike(Post post, AspNetUsers user);
    }
}
