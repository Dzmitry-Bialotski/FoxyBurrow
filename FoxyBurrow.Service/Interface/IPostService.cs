using FoxyBurrow.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoxyBurrow.Service.Interface
{
    public interface IPostService
    {
        Post Get(long id);
        IQueryable<Post> GetAll();
        IQueryable<Post> GetAllWithComments(User user);
        void Add(Post post);
        void Update(Post post);
        void Remove(long id);
        IQueryable<Post> SortedByPostedDate(IQueryable<Post> posts);

    }
}
