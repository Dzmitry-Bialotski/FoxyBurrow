using FoxyBurrow.Core.Entity;
using FoxyBurrow.Database.Repositpry;
using FoxyBurrow.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoxyBurrow.Service.Impl
{
    public class ChatService : IChatService
    {
        private readonly IRepository<Chat> _repository;

        public ChatService(IRepository<Chat> repository)
        {
            _repository = repository;
        }

        public void Add(Chat chat)
        {
            _repository.Add(chat);
        }

        public Chat Get(long id)
        {
            return _repository.Get(id);
        }

        public Chat Get(User user1, User user2)
        {
            return _repository.GetAll()
                .Where((r => 
                (r.FirstUser.Equals(user1) && r.SecondUser.Equals(user2)) 
                || (r.FirstUser.Equals(user2)) && r.SecondUser.Equals(user1)))
                .SingleOrDefault();
        }

        public IQueryable<Chat> GetAll()
        {
            return _repository.GetAll();
        }

        public IQueryable<Chat> GetAll(User user)
        {
            return _repository.GetAll().Where(c => c.FirstUser.Equals(user) || c.SecondUser.Equals(user));
        }

        public void Remove(long id)
        {
            _repository.Remove(Get(id));
        }

        public void Update(Chat chat)
        {
            _repository.Update(chat);
        }
    }
}
