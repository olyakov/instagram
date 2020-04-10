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
        private readonly IReport _reportService;

        private readonly IHostingEnvironment _he;


        public PostController(IPost postService,
            IRaiting raitingService,
            IHostingEnvironment he,
            IUser userService,
            IFollow followService,
            IComment commentService,
            IReport reportService)
        {
            _he = he;
            _postService = postService;
            _raitingService = raitingService;
            _userService = userService;
            _followService = followService;
            _commentService = commentService;
            _reportService = reportService;
        }

        public IActionResult Upload()
        {
            var model = new UploadPostModel();
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var post = _postService.GetById(id);
            ViewBag.Tags = string.Join(", ", post.Tags.Select(t => t.Title).ToList());

            return View("Edit", post);
        }

        [HttpPost]
        [Route("/post/edit")]
        public async Task<IActionResult> EditPost(string tags, string description, string title, string url, int id)
        {
            var post = _postService.GetById(id);
            post.Title = title;
            post.Description = description;
            post.Tags = _postService.ParseTags(tags);
            post.Url = url;

            await _postService.EditPost(post);
            return RedirectToAction("Detail", "Gallery", new{ id = post.Id});
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

        [HttpGet]
        [Route("{postId:int}/likes")]
        public ActionResult GetLikes(int postId)
        {
            var likes = _postService.GetById(postId)
                .Likes
                .ToList();

            return PartialView("_Likes", likes);
        }

        [HttpGet]
        [Route("{postId:int}/isLiked")]
        public ActionResult IsSetLike(int postId)
        {
            var post = _postService.GetById(postId);
            var isSetLike = post.Likes.Any(l => l.UserId == _userService.GetCurrentUser(HttpContext.User).Id);

            return PartialView("_HeartColor", isSetLike);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/post/add")]
        public async Task<IActionResult> UploadNewPost(IFormFile pic, string tags, string description, string title, string url)
        {

            if (pic != null)
            {
                var filename = Path.Combine(_he.WebRootPath, Path.GetFileName(pic.FileName));
                pic.CopyTo(new FileStream(filename, FileMode.Create));
                await _postService.AddPost(title, tags, description, "/" + Path.GetFileName(pic.FileName));
            }
            else if (url != null)
            {
                await _postService.AddPost(title, tags, description, url);
            }
            return RedirectToAction("Index", "Gallery");
        }

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

            var viewModel = new PostCommentorsViewModel()
            {
                UserId = user.Id,
                Comments = _commentService.GetPostComments(dto.PostId)
            };

            return PartialView("_CommentorsList", viewModel);
        }

        [HttpGet]
        [Route("{postId:int}/comments")]
        public ActionResult GetComments(int postId)
        {
            var comments = _postService.GetById(postId)
                .Comments
                .ToList();

            return PartialView("_Comments", comments);
        }

        [HttpPost]
        [Route("/post/report")]
        public ActionResult ReportPost(PostDto dto)
        {
            var post = _postService.GetById(dto.PostId);
            var user = _userService.GetCurrentUser(HttpContext.User);

            var report = new Report
            {
                ReportUserId = user.Id,
                ReportPost = post
            };

            _reportService.AddReport(report);

            return Ok();
        }

        [HttpGet]
        public IActionResult Search(string search)
        {
            var user = _userService.GetCurrentUser(HttpContext.User);
            var post = _postService
                .GetAll()
                .Where(p => (p.Title.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) 
                            || (string.Join(", ", p.Tags.Select(t => t.Title).ToList()).IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0))
                .OrderByDescending(p => p.Created).ToList()
                .Select(p => _postService.GetGalleryDetailModel(p));

            var model = new GalleryIndexModel()
            {
                Posts = post,
                User = user
            };

            return View("Newsline", model);
        }

    }
}