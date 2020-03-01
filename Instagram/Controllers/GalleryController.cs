using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instagram.Data;
using Instagram.Data.Model;
using Instagram.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}