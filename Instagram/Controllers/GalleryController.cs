using Instagram.Data;
using Instagram.Data.Model;
using Instagram.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Instagram.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IPost _postService;
        

        public GalleryController(IPost postService)
        {
            _postService = postService;
        }

        public IActionResult Index()
        {
            var postList = _postService.GetAll();   

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
                Tags = post.Tags.Select(t => t.Title).ToList()
                
            };
            return View(model);
        }
    }
}