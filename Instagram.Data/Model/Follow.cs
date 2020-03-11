using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Instagram.Data.Model
{
    public class Follow
    {
        public int Id { get; set; }

        public virtual AspNetUsers Follower { get; set; }
        public virtual AspNetUsers Following { get; set; }

        [Key]
        [Column(Order = 1)]
        public string FollowerId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string FollowingId { get; set; }

    }
}
