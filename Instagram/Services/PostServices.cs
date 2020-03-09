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

namespace Instagram.Services
{

    public class PostServices : IPost
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly InstagramDbContext _ctx;
        private readonly IdentityDbContext _userCtx;
        private readonly UserManager<AspNetUsers> _userManager;

        public PostServices(InstagramDbContext ctx, IdentityDbContext userCtx, UserManager<AspNetUsers> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _ctx = ctx;
            _userCtx = userCtx;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        [Authorize]
        public IEnumerable<Post> GetAll(string _username = null)
        {
            string userId;
            if (_username == null)
            {
                userId = ActionWithUser.GetCurrentUserAsync(_httpContextAccessor, _userManager).Result.Id;
            }
            else
            {
                userId = _userCtx.ApplicationUsers.FirstOrDefault(u => u.UserName == _username).Id;
            }

            return _ctx.Posts
                //.Where(p => p.UserId == userId)
                .Include(post => post.Tags)
                .Include(post => post.Likes);
        }

        public Post GetById(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }

        public void DeleteById(int id)
        {
            var del_post = GetAll().Where(p => p.Id == id).First();
            if (del_post != null)
            {
                _ctx.Posts.Remove(del_post);
                _ctx.SaveChanges();
            }
        }

        public IEnumerable<Post> GetWithTag(string tag)
        {
            return GetAll().Where(post
                => post.Tags
                    .Any(t => t.Title == tag));
        }

        public async Task AddPost(string title, string tags, string description, string url)
        {
            var httpcontext = _httpContextAccessor.HttpContext;
            var userId = (await _userManager.GetUserAsync(httpcontext.User)).Id;


            var post = new Post()
            {
                Title = title,
                Description = description,
                Tags = ParseTags(tags),
                Created = DateTime.Now,
                Url = url,
                UserId = userId,
                Likes = new List<Like>(),
                Dislikes = new List<Dislike>(),
                Comments = new List<Comment>()
            };
            _ctx.Posts.Add(post);
            await _ctx.SaveChangesAsync();
        }

        public List<Tag> ParseTags(string tags)
        {
            var httpcontext = _httpContextAccessor.HttpContext;
            var userId = _userManager.GetUserAsync(httpcontext.User).Id;

            return tags.Split(",").Select(tag => new Tag
            {
                Title = tag
            }).ToList();
        }
    }
}

