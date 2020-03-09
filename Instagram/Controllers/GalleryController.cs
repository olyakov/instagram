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

namespace Instagram.Controllers
{
    [Authorize("/Identity/Account/Login")]
    public class GalleryController : Controller
    {
        private readonly IPost _postService;
        

        public GalleryController(IPost postService)
        {
            _postService = postService;
        }


        [Route("/{username?}")]
        public IActionResult Index(string username)
        {
            
            var postList = _postService.GetAll(username);
            
            var model = new GalleryIndexModel()
            {
                Posts = postList
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var post = _postService.GetById(id);
            var pam = post.Likes == null ? new List<Like>() : post.Likes.ToList();
            var model = new GalleryDetailModel()
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Created = post.Created,
                Url = post.Url,
                Tags = post.Tags.Select(t => t.Title).ToList(),
                Likes = pam,
                Dislikes = post.Dislikes == null ? new List<Dislike>() : post.Dislikes.ToList(),
                Comments = post.Comments == null ? new List<Comment>() : post.Comments.ToList(),
                IsSetLike = post.Likes == null ? false : !post.Likes.Any(l => l.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value) ? false : true
            };
            return View(model);
        }
    }
}