using System.Collections.Generic;
using Instagram.Data;
using Instagram.Data.Model;
using Instagram.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Policy;
using Instagram.Services;

namespace Instagram.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        private readonly IPost _postService;
        private readonly IUser _userService;
        private readonly IFollow _followService;

        public GalleryController(IPost postService, IUser userService, IFollow followService)
        {
            _postService = postService;
            _userService = userService;
            _followService = followService;
        }


        [Route("/{username?}")]
        public IActionResult Index(string username)
        {
            var current_user = _userService.GetCurrentUser(HttpContext.User);
            if (username == null || username == "Home")
            {
                username = current_user.UserName;
            }
            var postList = _postService.GetAllByUsername(username);
            List<GalleryDetailModel> posts = new List<GalleryDetailModel>();
            foreach (var post in postList)
            {
                posts.Add(_postService.GetGalleryDetailModel(post));
            }

            var user = _userService.GetUserByUsername(username);
            var followers = _followService.GetUserFollows(user.Id);
            var followings = _followService.GetUserFollowings(user.Id);

            var model = new GalleryIndexModel()
            {
                Posts = posts,
                User = user,
                IsFollow = _followService.GetFollow( _userService.GetUserByUsername(username).Id, current_user.Id) == null ? false : true,
                Followers = followers,
                Followings = followings
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var post = _postService.GetById(id);
            var model = new GalleryDetailModel()
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Created = post.Created,
                Url = post.Url,
                Tags = post.Tags.Select(t => t.Title).ToList(),
                Likes = post.Likes.ToList(),
                Dislikes = post.Dislikes.ToList(),
                Comments = post.Comments.ToList(),
                IsSetLike = !post.Likes.Any(l => l.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value) ? false : true
            };
            return View(model);
        }
    }
}