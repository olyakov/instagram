using Instagram.Data;
using Instagram.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return GetAll().Where(p => p.Id == id)
                .First();
        }

        public IEnumerable<Post> GetWithTag(string tag)
        {
            return GetAll().Where(post
                => post.Tags
                    .Any(t => t.Title == tag));
        }

        public async Task SetPost(string title, string tags, string url)
        {
            var post = new Post()
            {
                Title = title,
                Tags = ParseTags(tags),
                Created = DateTime.Now,
                Url = url
            };
            _ctx.Add(post);
            await _ctx.SaveChangesAsync();
        }

        public List<Tag> ParseTags(string tags)
        {
            return tags.Split(",").Select(tag => new Tag
            {
                Title = tag
            }).ToList();
        }
    }
}
