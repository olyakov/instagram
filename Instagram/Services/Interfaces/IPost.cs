using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Instagram.Data.Model;
using Instagram.Models;

namespace Instagram.Services.Interfaces
{
    public interface IPost
    {
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetWithTag(string tag);
        IEnumerable<Post> GetAllByUsername(string username);
        IEnumerable<Post> GetAllCurrentUser();
        Post GetById(int id);
        Task Remove(int postId);
        Task AddPost(string title, string tags, string description, string url);
        List<Tag> ParseTags(string tags);
        GalleryDetailModel GetGalleryDetailModel(Post post);
        Task EditPost(Post post);
    }
}
