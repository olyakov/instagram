using System.Linq;
using System.Threading.Tasks;
using Instagram.Data;
using Instagram.Data.Model;
using Instagram.Models;
using Instagram.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUser _userService;
        private readonly IdentityDbContext _ctx;
        private readonly UserManager<AspNetUsers> _userManager;

        public AdminController(IUser userService,
            UserManager<AspNetUsers> userManager,
            IdentityDbContext ctx)
        {
            _userService = userService;
            _userManager = userManager;
            _ctx = ctx;
        }

        [Route("admin/reports/")]
        public IActionResult Reports()
        {
            var currentUser = _userService.GetCurrentUser(HttpContext.User);
            var users = _ctx.ApplicationUsers.Where(u => u.UserName != currentUser.UserName);

            var model = new AdminReportsViewModel
            {
                Users = users
            };

            return View(model);
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