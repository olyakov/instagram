using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Instagram.Data.Model
{
    public class Comment
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public virtual AspNetUsers User { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual Post Post { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }
    }
}
