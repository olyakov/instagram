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

            var model = new GalleryDetailModel()
            {
                Id = post.Id,
                Title = post.Title,
                Created = post.Created,
                Url = post.Url,
                Tags = post.Tags.Select(t => t.Title).ToList(),
                Likes = post.Likes == null ? new List<AspNetUsers>() : post.Likes.Select(l => l.Users).ToList(),
                Dislikes = post.Dislikes == null ? new List<AspNetUsers>() : post.Dislikes.Select(l => l.Users).ToList(),
                Comments = post.Comments == null ? new List<Comment>() : post.Comments.ToList()
            };
            return View(model);
        }
    }
}