using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instagram.Data.Model;
using Instagram.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IPost _postService;
        private readonly IUser _userService;
        private readonly UserManager<AspNetUsers> _userManager;

        public AdminController(IPost postService,
            IRaiting raitingService,
            IUser userService,
            UserManager<AspNetUsers> userManager)
        {
            _postService = postService;
            _userService = userService;
            _userManager = userManager;
        }

        [Route("admin/reports/")]
        public IActionResult Reports()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(string username)
        {
            if (username == User.Identity.Name)
                return BadRequest();

            var user = _userService.GetUserByUsername(username);
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Reports");
        }
    }
}