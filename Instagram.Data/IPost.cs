using System;
using System.Collections.Generic;
using System.Text;
using Instagram.Data.Model;

namespace Instagram.Data
{
    public interface IPost
    {
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetWithTag(string tag);
        Post GetById(int id);

    }
}
