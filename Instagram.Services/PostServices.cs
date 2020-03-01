using Instagram.Data;
using Instagram.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Services
{
    public class PostServices : IPost
    {
        private readonly InstagramDbContext _ctx;

        public PostServices(InstagramDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Post> GetAll()
        {
            return _ctx.Posts
                .Include(post => post.Tags);
        }

        public Post GetById(int id)
        {
            return _ctx.Posts.Find(id);
        }

        public IEnumerable<Post> GetWithTag(string tag)
        {
            return GetAll().Where(post
                => post.Tags
                    .Any(t => t.Title == tag));
        }
    }
}
