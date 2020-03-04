using Instagram.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using Instagram.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Instagram.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postService;
        private readonly IHostingEnvironment _he;

        public PostController(IPost postService, IHostingEnvironment he)
        {
            _he = he;
            _postService = postService;
        }

        public IActionResult Upload()
        {
            var model = new UploadPostModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadNewPost(IFormFile pic, string tags, string title)
        {
            if (pic != null)
            {
                var filename = Path.Combine(_he.WebRootPath, Path.GetFileName(pic.FileName));
                pic.CopyTo(new FileStream(filename, FileMode.Create));
                await _postService.SetPost(title, tags, "/"+Path.GetFileName(pic.FileName));
            }
            return RedirectToAction("Index", "Gallery");
        }
    }
}