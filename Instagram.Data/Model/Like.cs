using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Instagram.Data.Model
{
    public class Like
    {
        public string Id { get; set; }

        public virtual AspNetUsers User { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual Post Post { get; set; }

    }
}
