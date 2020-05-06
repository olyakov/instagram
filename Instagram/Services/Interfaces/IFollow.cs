using Instagram.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Instagram.Services.Interfaces
{
    public interface IFollow
    {
        Follow GetFollow(string followerId, string followingId);
        ICollection<Follow> GetUserFollows(string userId);
        ICollection<Follow> GetUserFollowings(string userId);
        ICollection<Follow> GetAll();
        bool IsFollow(string followerId, string followingId);
        void Add(Follow follow);
        void Remove(Follow follow);
    }
}
