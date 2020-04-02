using Instagram.Data.Model;
using Instagram.Models;
using Instagram.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Components
{
    public class ReportList : ViewComponent
    {
        private readonly IPost _postService;
        private readonly IUser _userService;
        private readonly IFollow _followService;
        private readonly IReport _reportService;

        public ReportList(IPost postService,
            IUser userService,
            IFollow followService,
            IReport reportService)
        {
            _postService = postService;
            _userService = userService;
            _followService = followService;
            _reportService = reportService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string username)
        {
            var reports = await Task.Run(() => _reportService.GetUserReport(username));
            var user = _userService.GetUserByUsername(username);

           //var posts = _postService.GetAllByUsername(username).OrderByDescending(u => u.Reports.Count());
  
            var model = new AdminReportsViewModel()
            {
                Reports = reports,
                User = user
            };


            return View("ReportList", model);
        }
    }
}
