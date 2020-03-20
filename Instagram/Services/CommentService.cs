using Instagram.Data;
using Instagram.Data.Model;
using Instagram.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
