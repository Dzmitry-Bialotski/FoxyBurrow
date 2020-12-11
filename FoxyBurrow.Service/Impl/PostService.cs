using FoxyBurrow.Core.Entity;
using FoxyBurrow.Database.Repositpry;
using FoxyBurrow.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoxyBurrow.Service.Impl
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> _repository;

        public PostService(IRepository<Post> repository)
        {
            _repository = repository;
        }
        public void Add(Post post)
        {
            _repository.Add(post);
        }

        public Post Get(long id)
        {
            return _repository.Get(id);
        }

        public IQueryable<Post> GetAll()
        {
            return _repository.GetAll();
        }

        public IQueryable<Post> GetAll(User user)
        {
            return _repository.GetAll().Where(p => p.User.Equals(user));
        }

        public void Remove(long id)
        {
            _repository.Remove(Get(id));
        }

        public IQueryable<Post> SortedByPostedDate(IQueryable<Post> posts)
        {
            return posts.OrderByDescending(s => s.MessageDate);
        }

        public void Update(Post post)
        {
            _repository.Update(post);
        }
    }
}
