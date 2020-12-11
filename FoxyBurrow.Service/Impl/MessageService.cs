using FoxyBurrow.Core.Entity;
using FoxyBurrow.Database.Repositpry;
using FoxyBurrow.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoxyBurrow.Service.Impl
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Message> _repository;

        public MessageService(IRepository<Message> repository)
        {
            _repository = repository;
        }
        public void Add(Message message)
        {
            _repository.Add(message);
        }

        public Message Get(long id)
        {
            return _repository.Get(id);
        }

        public IQueryable<Message> GetAll()
        {
            return _repository.GetAll();
        }

        public IQueryable<Message> GetAll(User user)
        {
            return _repository.GetAll().Where(c => c.User.Equals(user));
        }

        public IQueryable<Message> GetAll(Chat chat)
        {
            return _repository.GetAll().Where(c => c.Chat.Equals(chat));

        }

        public void Remove(long id)
        {
            _repository.Remove(Get(id));
        }

        public void Update(Message message)
        {
            _repository.Update(message);
        }
    }
}
