using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Instagram.Data.Model
{
    public class AspNetUsers : IdentityUser
    {
        public AspNetUsers()
        {
            Followings = new List<Follow>();
            Followers = new List<Follow>();
        }
        public virtual IEnumerable<Follow> Followings { get; set; }
        public virtual IEnumerable<Follow> Followers { get; set; }
    }
}
