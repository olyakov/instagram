﻿using Instagram.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Services.Interfaces
{
    public interface IComment
    {
        void AddComment(Comment comment);
        IEnumerable<Comment> GetPostComments(int postId);
    }
}
