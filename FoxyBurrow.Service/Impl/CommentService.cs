using FoxyBurrow.Core.Entity;
using FoxyBurrow.Database.Repository;
using FoxyBurrow.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoxyBurrow.Service.Impl
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _repository;
        public CommentService(IRepository<Comment> repository)
        {
            _repository = repository;
        }
        public void Add(Comment comment)
        {
            _repository.Add(comment);
        }

        public Comment Get(long id)
        {
            return _repository.Get(id);
        }

        public IQueryable<Comment> GetAll()
        {
            return _repository.GetAll();
        }

        public IQueryable<Comment> GetAll(User user)
        {
            return _repository.GetAll().Where(c => c.User.Equals(user));
        }

        public IQueryable<Comment> GetAll(Post post)
        {
            return _repository.GetAll()
                .Where(c => c.Post.Equals(post)).OrderByDescending(c => c.MessageDate);
        }

        public void Remove(long id)
        {
            _repository.Remove(Get(id));
        }

        public void Update(Comment comment)
        {
            _repository.Update(comment);
        }
    }
}
