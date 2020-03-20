using Instagram.Models;
using Instagram.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Instagram.Components
{
    public class PostsList : ViewComponent
    {
        private readonly IPost _postService;
        private readonly IUser _userService;
        private readonly IFollow _followService;

        public PostsList(IPost postService, IUser userService, IFollow followService)
        {
            _postService = postService;
            _userService = userService;
            _followService = followService;
        }

        public IViewComponentResult Invoke()
        {
            var user = _userService.GetCurrentUser(HttpContext.User);
            var followings = _followService
                .GetUserFollowings(user.Id)
                .Select(f => f.FollowerId);

            var posts = _postService
                .GetAll()
                .Where(p => followings.Contains(p.UserId))
                .OrderByDescending(p => p.Created).ToList()
                .Select(p => _postService.GetGalleryDetailModel(p));

            int maxLikes = posts.Max(p => p.Likes.Count);

            // если передан параметр id
            if (RouteData.Values.ContainsKey("id"))
                Int32.TryParse(RouteData.Values["id"]?.ToString(), out maxLikes);

            posts.Where(p => p.Likes.Count <= maxLikes).ToList();
            ViewData["Header"] = $"Posts with likes number up to {maxLikes.ToString("c")}";

            var model = new GalleryIndexModel()
            {
                Posts = posts,
                User = user
            };
            ViewBag.Model = model;

            return View();
        }
    }
}
