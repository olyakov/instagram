using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Instagram.Data.Model
{
    public class Report
    {
        public int Id { get; set; }

        public virtual AspNetUsers ReportUser { get; set; }
        public virtual Post ReportPost{ get; set; }

        [Required]
        public string ReportUserId { get; set; }
    }
}
