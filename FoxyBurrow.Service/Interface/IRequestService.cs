using FoxyBurrow.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoxyBurrow.Service.Interface
{
    public interface IRequestService
    {
        Request Get(long id);
        Request Get(User user1, User user2);
        IQueryable<Request> GetAll();
        IQueryable<Request> GetAllByUser(User user);
        IQueryable<User> GetAllOutgoingByUser(User user);
        IQueryable<User> GetAllIncomingByUser(User user);
        void Add(Request request);
        void Update(Request request);
        void Remove(long id);
        IQueryable<User> GetUserFriends(User user);
        bool IsFriend(User user, User friend);
        bool IsFollower(User user, User friend);
        void AddFriend(User user, User friend);
        void DeleteFriend(User user, User friend);

    }
}
