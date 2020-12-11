using FoxyBurrow.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoxyBurrow.Service.Interface
{
    public interface IMessageService
    {
        Message Get(long id);
        IQueryable<Message> GetAll();
        IQueryable<Message> GetAll(User user);
        IQueryable<Message> GetAll(Chat chat);
        void Add(Message message);
        void Update(Message message);
        void Remove(long id);
    }
}
