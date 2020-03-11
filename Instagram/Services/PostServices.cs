using Instagram.Data;
using Instagram.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Instagram.Models;

namespace Instagram.Services
{

    public class PostServices : IPost
    {

        private readonly InstagramDbContext _ctx;
        private readonly IUser _userService;
        private readonly IHttpContextAccessor _httpCtxAcc;



        public PostServices(InstagramDbContext ctx, IUser userService, IHttpContextAccessor httpCtxAcc)
        {
            _ctx = ctx;
            _userService = userService;
            _httpCtxAcc = httpCtxAcc;
        }

        public IEnumerable<Post> GetAll() => _ctx.Posts
                .Include(post => post.Tags)
                .Include(post => post.User)
                .Include(post => post.Likes);


        public IEnumerable<Post> GetAllCurrentUser()
        {
            var userId = _userService.GetCurrentUser(_httpCtxAcc.HttpContext.User).Result.Id;

            return GetAll().Where(p => p.UserId == userId);
        }

        public IEnumerable<Post> GetAllByUsername(string username)
        {
            var userId = _userService.GetUserByUsername(username).Id;

            return GetAll().Where(p => p.UserId == userId);
        }

        public Post GetById(int id) => GetAll().FirstOrDefault(p => p.Id == id);


        public async Task DeleteById(int id)
        {
            var del_post = GetAllCurrentUser().FirstOrDefault(p => p.Id == id);
            _ctx.Posts.Remove(del_post);
            await _ctx.SaveChangesAsync();
        }

        public IEnumerable<Post> GetWithTag(string tag)
        {
            return GetAll().Where(post
                => post.Tags
                    .Any(t => t.Title == tag));
        }

        public async Task AddPost(string title, string tags, string description, string url)
        {
            var userId = _userService.GetCurrentUser(_httpCtxAcc.HttpContext.User).Result.Id;

            var post = new Post()
            {
                Title = title,
                Description = description,
                Tags = ParseTags(tags),
                Created = DateTime.Now,
                Url = url,
                UserId = userId
            };
            _ctx.Posts.Add(post);
            await _ctx.SaveChangesAsync();
        }

        public List<Tag> ParseTags(string tags) => tags.Split(",").Select(tag => new Tag
        {
            Title = tag
        }).ToList();

        public GalleryDetailModel GetGalleryDetailModel(Post post)
        {
            var model = new GalleryDetailModel()
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Created = post.Created,
                Url = post.Url,
                Tags = post.Tags.Select(t => t.Title).ToList(),
                Likes = post.Likes == null ? new List<Like>() : post.Likes.ToList(),
                Dislikes = post.Dislikes == null ? new List<Dislike>() : post.Dislikes.ToList(),
                Comments = post.Comments == null ? new List<Comment>() : post.Comments.ToList(),
                User = post.User,
                IsSetLike = post.Likes == null ? false : !post.Likes.Any(l => l.UserId == _userService.GetCurrentUser(_httpCtxAcc.HttpContext.User).Result.Id) ? false : true
            };
            return model;
        }

    }
}

