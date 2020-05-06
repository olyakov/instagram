using Instagram.Data;
using Instagram.Data.Model;
using Instagram.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Services
{
    class ReportService : IReport
    {
        private readonly InstagramDbContext _ctx;

        public ReportService(InstagramDbContext ctx)
        {
            _ctx = ctx;
        }

        public void AddReport(Report report)
        {
            _ctx.Reports.Add(report);
            _ctx.SaveChanges();
        }

        public IEnumerable<Report> GetUserReport(string username) => _ctx.Reports
            .Include(r => r.ReportPost)
            .Include(r => r.ReportUser)
            .Where(r => r.ReportPost.User.UserName == username);

    }
}
