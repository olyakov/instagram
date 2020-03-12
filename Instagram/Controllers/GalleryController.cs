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


        public GalleryController(IPost postService, IUser userService)
        {
            _postService = postService;
            _userService = userService;
        }


        [Route("/{username?}")]
        public IActionResult Index(string username)
        {
            if (username == null)
            {
                username = User.FindFirst(ClaimTypes.Name).Value;
            }
            var postList = _postService.GetAllByUsername(username);
            List<GalleryDetailModel> posts = new List<GalleryDetailModel>();
            foreach (var post in postList)
            {
                posts.Add(_postService.GetGalleryDetailModel(post));
            }
            
            var model = new GalleryIndexModel()
            {
                Posts = posts,
                User = _userService.GetCurrentUser(HttpContext.User).Result
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
                Likes = post.Likes == null ? new List<Like>() : post.Likes.ToList(),
                Dislikes = post.Dislikes == null ? new List<Dislike>() : post.Dislikes.ToList(),
                Comments = post.Comments == null ? new List<Comment>() : post.Comments.ToList(),
                IsSetLike = post.Likes == null ? false : !post.Likes.Any(l => l.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value) ? false : true
            };
            return View(model);
        }
    }
}