using Instagram.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class GalleryIndexModel
    {
        public IEnumerable<GalleryDetailModel> Posts { get; set; }
        public AspNetUsers User { get; set; }
        public ICollection<Follow> Followings { get; set; }
        public ICollection<Follow> Followers { get; set; }
        public bool IsFollow { get; set; }
    }
}
