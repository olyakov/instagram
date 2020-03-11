using Instagram.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using Instagram.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net;
using Instagram.Services;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;

namespace Instagram.Controllers
{
    public class LikeDto
    {
        public int PostId { get; set; }
    }

    public class PostController : Controller
    {
        private readonly IPost _postService;
        private readonly IRaiting _raitingService;
        private readonly IUser _userService;
        private readonly IHostingEnvironment _he;


        public PostController(IPost postService, IRaiting raitingService, IHostingEnvironment he,
            IUser userService)
        {
            _he = he;
            _postService = postService;
            _raitingService = raitingService;
            _userService = userService;
        }

        public IActionResult Upload()
        {
            var model = new UploadPostModel();
            return View(model);
        }   

        public IActionResult Newsline()
        {
            var postList = _postService.GetAll().OrderByDescending(p => p.Created).ToList();
            List<GalleryDetailModel> posts = new List<GalleryDetailModel>();
            foreach (var post in postList)
            {
                posts.Add(_postService.GetGalleryDetailModel(post));
            }

            var model = new GalleryIndexModel()
            {
                Posts = posts
            };

            return View(model);
        }
 
        [HttpPost]
        public async Task<IActionResult> SetLike(LikeDto dto)
        {
            var post = _postService.GetById(dto.PostId);
            var user = _userService.GetCurrentUser(HttpContext.User).Result;
            await _raitingService.SetLike(post, user);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UploadNewPost(IFormFile pic, string tags, string description, string title)
        {
            if (pic != null)
            {
                var filename = Path.Combine(_he.WebRootPath, Path.GetFileName(pic.FileName));
                pic.CopyTo(new FileStream(filename, FileMode.Create));
                await _postService.AddPost(title, tags, description, "/" + Path.GetFileName(pic.FileName));
            }
            return RedirectToAction("Index", "Gallery");
        }
    }
}