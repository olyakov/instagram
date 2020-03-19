using Instagram.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class PostCommentorsViewModel
    {
        public string UserId { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
