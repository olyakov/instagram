using Instagram.Data;
using Instagram.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Services
{
    public class CommentService : IComment
    {
        private readonly InstagramDbContext _ctx;

        public CommentService(InstagramDbContext ctx)
        {
            _ctx = ctx;
        }

        public void AddComment(Comment comment)
        {
            _ctx.Comments.Add(comment);
            _ctx.SaveChanges();
        }

        public IEnumerable<Comment> GetPostComments(int postId) => _ctx.Comments
            .Include(c => c.User)
            .Where(predicate: c => c.PostId == postId);
    }
}
