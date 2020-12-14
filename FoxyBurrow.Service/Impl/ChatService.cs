using FoxyBurrow.Core.Entity;
using FoxyBurrow.Database.Repository;
using FoxyBurrow.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FoxyBurrow.Service.Impl
{
    public class ChatService : IChatService
    {
        private readonly IRepository<Chat> _repository;
        private readonly IUserService _userService;

        public ChatService(IRepository<Chat> repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        public void Add(Chat chat)
        {
            _repository.Add(chat);
        }

        public Chat Get(long id)
        {
            return _repository.Get(id);
        }

        public IQueryable<Chat> GetAll()
        {
            return _repository.GetAll();
        }

        public void Remove(long id)
        {
            _repository.Remove(Get(id));
        }

        public void Update(Chat chat)
        {
            _repository.Update(chat);
        }
        ///
        public bool HasChat(User user1, User user2)
        {
            return _repository.GetAll()
               .Where((r =>
               (r.FirstUserId == user1.Id && r.SecondUserId == user2.Id)
               || (r.FirstUserId == user2.Id && r.SecondUserId == user1.Id))).Count() > 0;
        }
        public IQueryable<Chat> GetAll(User user)
        {
            return _repository.GetAll().Where(c => c.FirstUserId == user.Id || c.SecondUserId == user.Id)
                    .Include(c => c.Messages);
        }
        public Chat Get(User user1, User user2)
        {
            return _repository.GetAll()
                .Where((r =>
                (r.FirstUserId == user1.Id && r.SecondUserId == user2.Id)
                || (r.FirstUserId == user2.Id && r.SecondUserId == user1.Id))).Include(c => c.Messages)
                .SingleOrDefault();
        }
        public Chat GetOrCreate(User user1, User user2)
        {
            if(HasChat(user1,user2))
            {
                return Get(user1, user2);
            }
            else
            {
                Chat chat = new Chat()
                {
                    FirstUser = user1,
                    SecondUser = user2
                };
                Update(chat);
                return chat;
            }
        }
        public async Task<User> GetOpponent(Chat chat, User user)
        {
            if (chat.FirstUserId == user.Id)
            {
                return await _userService.GetAsync(chat.SecondUserId);
            }
            else if (chat.SecondUserId == user.Id)
            {
                return await _userService.GetAsync(chat.FirstUserId);
            }
            else return null;
        }
    }
}
