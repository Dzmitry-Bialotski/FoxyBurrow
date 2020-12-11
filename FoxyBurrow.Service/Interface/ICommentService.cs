using FoxyBurrow.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoxyBurrow.Service.Interface
{
    public interface ICommentService
    {
        Comment Get(long id);
        IQueryable<Comment> GetAll();
        IQueryable<Comment> GetAll(User user);
        IQueryable<Comment> GetAll(Post post);
        void Add(Comment comment);
        void Update(Comment comment);
        void Remove(long id);

    }
}
