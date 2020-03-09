using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Instagram.Data.Model;

namespace Instagram.Data
{
    public interface IPost
    {
        IEnumerable<Post> GetAll(string userId);
        IEnumerable<Post> GetWithTag(string tag);
        Post GetById(int id);
        void DeleteById(int id);
        Task AddPost(string title, string tags, string description, string url);
        List<Tag> ParseTags(string tags);
    }
}
