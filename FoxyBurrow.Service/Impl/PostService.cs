using FoxyBurrow.Core.Entity;
using FoxyBurrow.Database.Repository;
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
        private readonly ICommentService _commentService;

        public PostService(IRepository<Post> repository, ICommentService commentService)
        {
            _repository = repository;
            _commentService = commentService;
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

        public IQueryable<Post> GetAllWithComments(User user)
        {
            IQueryable<Post> posts = _repository.GetAll().Where(p => p.User.Equals(user));
            foreach(Post post in posts)
            {
                post.Comments = _commentService.GetAll(post).ToList();
            }
            return posts;
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
