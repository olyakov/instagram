using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Instagram.Data.Model;
using Instagram.Models;
using Instagram.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Controllers
{
    
    public class FollowDto
    {
        public string FollowingId { get; set; }
        public string Username { get; set; }
    }
    
    [Authorize]
    public class FollowsController : Controller
    {

        private readonly IUser _userService;
        private readonly IFollow _followService;

        public FollowsController(IFollow followService, IUser userService)
        {
            _userService = userService;
            _followService = followService;
        }

        [HttpPost]
        public IActionResult Following(FollowDto dto)
        {
            var follower = _userService.GetCurrentUser(HttpContext.User);

            var follow = new Follow
            {
                FollowerId = dto.FollowingId,
                FollowingId= follower.Id
            };
            var del_follow = _followService.GetFollow(follow.FollowerId, follow.FollowingId);
            if (del_follow == null)
            {
                _followService.Add(follow);
            }
            else
            {
                _followService.Remove(del_follow);
            }
            

            return Ok();
        }

        [HttpGet]
        public IActionResult GetFollowings(FollowDto dto)
        {
            var user = _userService.GetUserByUsername(dto.Username);
            var followings = _followService.GetUserFollowings(user.Id).Select(f => f.FollowerId);

            List<AspNetUsers> users = new List<AspNetUsers>();

            foreach (var userId in followings)
            {
                users.Add(_userService.GetUserById(userId));
            }

            var viewmodel = new PostUserViewModel()
            {
                UserId = user.Id,
                Users = users
            };

            return PartialView("_UserList", viewmodel);
        }

        [HttpGet]
        public IActionResult GetFollowers(FollowDto dto)
        {
            var user = _userService.GetUserByUsername(dto.Username);
            var followers = _followService.GetUserFollows(user.Id).Select(f => f.FollowingId);

            List<AspNetUsers> users = new List<AspNetUsers>();

            foreach (var userId in followers)
            {
                users.Add(_userService.GetUserById(userId));
            }

            var viewmodel = new PostUserViewModel()
            {
                UserId = user.Id,
                Users = users
            };

            return PartialView("_UserList", viewmodel);
        }

    }
}