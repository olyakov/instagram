using Instagram.Models;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Upload()
        {
            var model = new UploadPostModel();
            return View();
        }

        [HttpPost]
        public IActionResult UploadNewPost()
        {
            return Ok();
        }
    }
}