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
        public virtual ICollection<Follow> Followings { get; set; }
        public virtual ICollection<Follow> Followers { get; set; }
    }
}
