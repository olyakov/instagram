using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Instagram.Data.Model
{
    public class Post
    {
        public int Id { get; set; }

        public AspNetUsers User { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public DateTime Created{ get; set; }

        [Required]
        public string Url { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Like> Likes { get; set; }
        public IEnumerable<Dislike> Dislikes { get; set; }

        

        
    }
}
