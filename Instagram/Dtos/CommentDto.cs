using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Dtos
{
    public class CommentDto
    {
        public string Message { get; set; }
        public string CommenterId { get; set; }
        public int PostId { get; set; }
    }
}
