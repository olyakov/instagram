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

        public IEnumerable<Post> GetAll(string _username)
        {
            string userId;
            if (_username == null)
            {
                userId = ActionWithUser.GetCurrentUserAsync(_httpContextAccessor, _userManager).Result.Id;
            }
            else
            {
                userId = _userCtx.ApplicationUsers.Where(u => u.UserName == _username).First().Id;
            }
            
            return _ctx.Posts
                .Where(p => p.UserId == userId)
                .Include(post => post.Tags);
        }

        public Post GetById(int id)
        {
            return GetAll(null).Where(p => p.Id == id)
                .First();
        }

        public void DeleteById(int id)
        {
            var del_post = GetAll(null).Where(p => p.Id == id).First();
            if (del_post != null)
            {
                _ctx.Posts.Remove(del_post);
                _ctx.SaveChanges();
            }
        }

        public IEnumerable<Post> GetWithTag(string tag)
        {
            return GetAll(null).Where(post
                => post.Tags
                    .Any(t => t.Title == tag));
        }

        [Authorize]
        public async Task AddPost(string title, string tags, string url)
        {
            var httpcontext = _httpContextAccessor.HttpContext;
            var userId = (await _userManager.GetUserAsync(httpcontext.User)).Id;


            var post = new Post()
            {
                Title = title,
                Tags = ParseTags(tags),
                Created = DateTime.Now,
                Url = url,
                UserId = userId
            };
            _ctx.Posts.Add(post);
            await _ctx.SaveChangesAsync();
        }

        public List<Tag> ParseTags(string tags)
        {
            var httpcontext = _httpContextAccessor.HttpContext;
            var userId =  _userManager.GetUserAsync(httpcontext.User).Id;

            return tags.Split(",").Select(tag => new Tag
            {
                Title = tag
            }).ToList();
        }

        public void SetLike(int postId)
        {
            string userId = ActionWithUser.GetCurrentUserAsync(_httpContextAccessor, _userManager).Result.Id;
            Like like = new Like()
            {
                UserId = userId

            };
            if (GetById(postId).Likes
                    .Where(l => l.UserId == userId) != null)
            {
                _ctx.Likes.Remove(like);
            }
            else
            {
                _ctx.Likes.Add(like);
            }
            _ctx.SaveChanges();
        }
    }
}
