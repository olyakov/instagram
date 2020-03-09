using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Instagram.Data.Model;

namespace Instagram.Services
{
    public interface IPost
    {
        IEnumerable<Post> GetAll(string userId);
        IEnumerable<Post> GetWithTag(string tag);
        Post GetById(int id);
        Task DeleteById(int id);
        Task AddPost(string title, string tags, string description, string url);
        List<Tag> ParseTags(string tags);
    }
}
