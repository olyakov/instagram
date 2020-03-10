using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Instagram.Data.Model;
using Instagram.Models;

namespace Instagram.Services
{
    public interface IPost
    {
        IEnumerable<Post> GetAll(string userId=null);
        IEnumerable<Post> GetWithTag(string tag);
        Post GetById(int id);
        Task DeleteById(int id);
        Task AddPost(string title, string tags, string description, string url);
        List<Tag> ParseTags(string tags);
        GalleryDetailModel GetGalleryDetailModel(Post post);
    }
}
