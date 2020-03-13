using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instagram.Data.Model;
using Instagram.Services;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Controllers
{
    
    public class FollowDto
    {
        public string FollowingId { get; set; }
    }
    
    public class FollowsController : Controller
    {

        private readonly IUser _userService;
        private readonly IFollow _followingService;

        public FollowsController(IFollow followingService, IUser userService)
        {
            _userService = userService;
            _followingService = followingService;
        }

        [HttpPost]
        public IActionResult Following(FollowDto dto)
        {
            var follower = _userService.GetCurrentUser(HttpContext.User);

            var follow = new Follow
            {
                FollowingId = dto.FollowingId,
                FollowerId = follower.Id
            };
            var del_follow = _followingService.GetFollow(follow.FollowerId, follow.FollowingId);
            if (del_follow == null)
            {
                _followingService.Add(follow);
            }
            else
            {
                _followingService.Remove(del_follow);
            }
            

            return Ok();
        }

      /*  [HttpDelete]
        public IActionResult UnFollow(string followingId)
        {
            var follower = _userService.GetCurrentUser(HttpContext.User);

            var follow = _followingService.GetFollow(follower.Id, followingId);

            

            return Ok();
        }
        */
    }
}