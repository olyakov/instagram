using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Instagram.Data.Model;

namespace Instagram.Data
{
    public interface IPost
    {
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetWithTag(string tag);
        Post GetById(int id);
        Task SetPost(string title, string tags, string url);
        List<Tag> ParseTags(string tags);
    }
}
