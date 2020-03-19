using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instagram.Data.Model;

namespace Instagram.Models
{
    public class PostUserViewModel
    {
        public string UserId { get; set; }
        public IEnumerable<AspNetUsers> Users { get; set; }
        public ICollection<Follow> Followings { get; set; }
        public ICollection<Follow> Followers { get; set; }
    }
}
