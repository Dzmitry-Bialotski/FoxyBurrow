using FoxyBurrow.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoxyBurrow.Service.Interface
{
    public interface IChatService
    {
        Chat Get(long id);
        Chat Get(User user1, User user2);
        IQueryable<Chat> GetAll();
        IQueryable<Chat> GetAll(User user);
        void Add(Chat chat);
        void Update(Chat chat);
        void Remove(long id);
        Chat GetOrCreate(User user1, User user2);
    }
}