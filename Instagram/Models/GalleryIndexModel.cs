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
    }
}
