using Instagram.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Models
{
    public class AdminReportsViewModel
    {
        public IEnumerable<AspNetUsers> Users { get; set; }
        public IEnumerable<Report> Reports { get; set; }
        public AspNetUsers User { get; set; }
    }
}
