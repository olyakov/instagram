using Instagram.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Instagram.Services
{
    public interface IFollow
    {
        Follow GetFollow(string followerId, string followingId);
        IEnumerable<Follow> GetUserFollows(string userId);
        IEnumerable<Follow> GetAll();
        bool IsFollow(string followerId, string followingId);
        void Add(Follow follow);
        void Remove(Follow follow);
    }
}
