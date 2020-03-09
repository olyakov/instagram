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

        public IEnumerable<Post> GetAll(string _username = null)
        {
            string userId;
            if (_username == null)
            {
                userId = _userService.GetCurrentUser(_httpCtxAcc.HttpContext.User).Result.Id;
            }
            else
            {
                userId = _userService.GetUserByUsername(_username).Id;
            }

            return _ctx.Posts
                //.Where(p => p.UserId == userId)
                .Include(post => post.Tags)
                .Include(post => post.Likes);
        }

        public Post GetById(int id) => GetAll().FirstOrDefault(p => p.Id == id);


        public async Task DeleteById(int id)
        {
            var del_post = GetAll().FirstOrDefault(p => p.Id == id);
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
        
    }
}

