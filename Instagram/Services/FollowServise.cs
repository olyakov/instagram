using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Instagram.Data;
using Instagram.Data.Model;

namespace Instagram.Services
{
    public class FollowServise : IFollow
    {
        private readonly InstagramDbContext _context;

        public FollowServise(InstagramDbContext context)
        {
            _context = context;
        }

        public Follow GetFollow(string followerId, string followingId) =>
            _context.Follows.FirstOrDefault(f => f.FollowerId == followerId &&
                                                    f.FollowingId == followingId);

        public IEnumerable<Follow> GetUserFollows(string userId) =>
            _context.Follows.Where(f => f.FollowerId == userId);

        public IEnumerable<Follow> GetAll() => _context.Follows;

        public bool IsFollow(string followerId, string followingId) =>
            _context.Follows.Any(f => f.FollowingId == followingId &&
                                         f.FollowerId == followerId);

        public void Add(Follow follow)
        {
            _context.Follows.Add(follow);
            _context.SaveChanges();
        }

        public void Remove(Follow follow)
        {
            _context.Follows.Remove(follow);
            _context.SaveChanges();
        }
    }
}
