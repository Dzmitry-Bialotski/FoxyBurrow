using FoxyBurrow.Core.Entity;
using FoxyBurrow.Core.Enum;
using FoxyBurrow.Database.Repositpry;
using FoxyBurrow.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoxyBurrow.Service.Impl
{
    public class RequestService : IRequestService
    {
        private readonly IRepository<Request> _repository;

        public RequestService(IRepository<Request> repository)
        {
            _repository = repository;
        }
        public void Add(Request request)
        {
            _repository.Add(request);
        }

        public Request Get(long id)
        {
            return _repository.Get(id);
        }

        public Request Get(User user1, User user2)
        {
            return _repository.GetAll().Where((r => (r.UserSender == user1 && r.UserReceiver == user2) || (r.UserSender == user2 && r.UserReceiver == user1))).SingleOrDefault();
        }

        public IQueryable<Request> GetAll()
        {
            return _repository.GetAll();
        }

        public IQueryable<Request> GetAllByUser(User user)
        {
            return _repository.GetAll()
                .Where(r => (r.UserSender.Equals(user) && r.RequestType == RequestType.WAITING)
                || (r.UserReceiver.Equals(user) && r.RequestType == RequestType.WAITING));

        }

        public IQueryable<Request> GetAllIncomingByUser(User user)
        {
            return _repository.GetAll().Where(r => r.UserSender.Equals(user)
            && r.RequestType == RequestType.WAITING);
        }

        public IQueryable<Request> GetAllOutgoingByUser(User user)
        {
            return _repository.GetAll().Where(r => r.UserReceiver.Equals(user) 
            && r.RequestType == RequestType.WAITING);

        }

        public IQueryable<User> GetUserFriends(User user)
        {
            return _repository.GetAll()
                 .Where(r => r.UserSender.Equals(user) && r.RequestType == RequestType.CONFIRMED)
                 .Select(r => r.UserReceiver)
                 .Union(_repository.GetAll()
                 .Where(r => r.UserReceiver.Equals(user) && r.RequestType == RequestType.CONFIRMED)
                 .Select(r => r.UserSender));
        }

        public bool IsRequestExistence(User user1, User user2)
        {
            return _repository.GetAll()
                    .Where((r => ((r.UserSender == user1 && r.UserReceiver == user2)
                    && r.RequestType == RequestType.WAITING)
                    || ((r.UserSender == user2 && r.UserReceiver == user1)
                    && r.RequestType == RequestType.WAITING))).Count() > 0
                    ? true : false;
        }

        public void Remove(long id)
        {
            _repository.Remove(Get(id));
        }

        public void Update(Request request)
        {
            _repository.Update(request);
        }

        public bool UserInFriendsList(IQueryable<User> friends, User user)
        {
            return friends.Contains(user);
        }
    }
}
