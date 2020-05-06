using Instagram.Data;
using Instagram.Data.Model;
using Instagram.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Services
{
    public class RaitingService : IRaiting
    {
        private readonly InstagramDbContext _ctx;

        public RaitingService(InstagramDbContext ctx)
        {
            _ctx = ctx;
        }


        public async Task SetLike(Post post, AspNetUsers user)
        {
            // var user = ActionWithUser.GetCurrentUserAsync(_httpContextAccessor, _userManager).Result;
            // var post = GetById(postId);

            Like like = new Like()
            {
                UserId = user.Id,
                Post = post
            };

            bool truth = true;

            truth = post.Likes != null && post.Likes.Any(l => l.UserId == user.Id);

            if (truth)
            {
                var del_like = post.Likes.FirstOrDefault(l => l.UserId == user.Id);
                _ctx.Likes.Remove(del_like);
                _ctx.SaveChanges();
            }
            else
            {
                _ctx.Likes.Add(like);
                await _ctx.SaveChangesAsync();
                /* var updatePost = GetById(postId);
                 if (updatePost.Likes == null)
                     updatePost.Likes = new List<Like>();*/
            }
        }
    }
}

