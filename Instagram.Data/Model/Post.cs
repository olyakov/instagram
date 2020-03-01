using System;
using System.Collections.Generic;
using System.Text;

namespace Instagram.Data.Model
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Created{ get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public string Url { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

        
    }
}
