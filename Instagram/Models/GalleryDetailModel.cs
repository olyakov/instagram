﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instagram.Data.Model;

namespace Instagram.Models
{
    public class GalleryDetailModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string Url { get; set; }

        public List<string> Tags { get; set; }
        public List<Like> Likes { get; set; }
        public List<Dislike> Dislikes { get; set; }
        public List<Comment> Comments { get; set; }
        public AspNetUsers User { get; set; }
        
        public bool IsSetLike { get; set; }
        public bool IsFollow { get; set; }
    }
}
