using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instagram.Data.Model;

namespace Instagram.Models
{
    public class PostUserViewModel
    {
        public string UserId { get; internal set; }
        public IEnumerable<AspNetUsers> Users { get; internal set; }
        public IEnumerable<Follow> Followings { get; internal set; }
    }
}
