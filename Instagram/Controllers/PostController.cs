using Instagram.Data.Model;
using Instagram.Dtos;
using Instagram.Models;
using Instagram.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Controllers
{

    [Authorize]
    public class PostController : Controller
    {
        private readonly IPost _postService;
        private readonly IRaiting _raitingService;
        private readonly IUser _userService;
        private readonly IFollow _followService;
        private readonly IComment _commentService;

        private readonly IHostingEnvironment _he;


        public PostController(IPost postService, IRaiting raitingService,IHostingEnvironment he,
            IUser userService, IFollow followService, IComment commentService)
        {
            _he = he;
            _postService = postService;
            _raitingService = raitingService;
            _userService = userService;
            _followService = followService;
            _commentService = commentService;
        }

        public IActionResult Upload()
        {
            var model = new UploadPostModel();
            return View(model);
        }   

        public IActionResult Newsline()
        {
            var user = _userService.GetCurrentUser(HttpContext.User);
            var followings = _followService
                .GetUserFollowings(user.Id)
                .Select(f => f.FollowerId);

            var posts = _postService
                .GetAll()
                .Where(p => followings.Contains(p.UserId))
                .OrderByDescending(p => p.Created).ToList()
                .Select(p => _postService.GetGalleryDetailModel(p));

            var model = new GalleryIndexModel()
            {
                Posts = posts,
                User = user
            };

            return View(model);
        }
 
        [HttpPost]
        public async Task<IActionResult> SetLike(PostDto dto)
        {
            var post = _postService.GetById(dto.PostId);
            var user = _userService.GetCurrentUser(HttpContext.User);
            await _raitingService.SetLike(post, user);
            return Ok();
        }


        [HttpGet]
        public ActionResult GetLikers(PostDto dto)
        {
            var post = _postService.GetById(dto.PostId);
            var user = _userService.GetUserByUsername(post.User.UserName);

            var likers = post.Likes
                .Select(r => _userService.GetUserById(r.UserId))
                .ToList();
 
            var viewModel = new PostUserViewModel()
            {
                UserId = user.Id,
                Users = likers
            };

            return PartialView("_UserList", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/post/add")]
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
        
        [HttpDelete]
        [Route("/remove/{id:int}")]
        public async Task<IActionResult> RemovePost(int id)
        {
            await _postService.Remove(id);
            return RedirectToAction("Index", "Gallery");
        }

        [HttpPost]
        [Route("/api/post/comment")]
        public ActionResult AddComment(CommentDto dto)
        {
            var comment = new Comment
            {
                PostId = dto.PostId,
                UserId = dto.CommenterId,
                Content = dto.Message,
                Created = DateTime.Now
            };
            _commentService.AddComment(comment);
            return Ok();
        }

        [HttpGet]
        [Route("/post/show/comments")]
        public ActionResult GetCommentors(PostDto dto)
        {
            var post = _postService.GetById(dto.PostId);
            var user = _userService.GetUserByUsername(post.User.UserName);

            var commentors = _commentService
                .GetPostComments(dto.PostId)
                .OrderByDescending(c => c.Created);

            var viewModel = new PostCommentorsViewModel()
            {
                UserId = user.Id,
                Comments = commentors
            };

            return PartialView("_CommentorsList", viewModel);

        }

    }
}