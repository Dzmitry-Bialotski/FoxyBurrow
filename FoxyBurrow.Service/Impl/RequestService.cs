using FoxyBurrow.Core.Entity;
using FoxyBurrow.Core.Enum;
using FoxyBurrow.Database.Repository;
using FoxyBurrow.Service.Interface;
using Microsoft.EntityFrameworkCore;
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
                .Where(r => (r.UserSender.Id == user.Id && r.RequestType == RequestType.WAITING)
                || (r.UserReceiver.Id == user.Id && r.RequestType == RequestType.WAITING));

        }

        public IQueryable<User> GetAllIncomingByUser(User user)
        {
            return _repository.GetAll().Where(r => r.UserReceiver.Id == user.Id
            && r.RequestType == RequestType.WAITING).Select(r => r.UserSender);
        }

        public IQueryable<User> GetAllOutgoingByUser(User user)
        {
            return _repository.GetAll().Where(r => r.UserSender.Id == user.Id
            && r.RequestType == RequestType.WAITING).Select(r => r.UserReceiver);
        }

        public IQueryable<User> GetUserFriends(User user)
        {
            return _repository.GetAll()
                 .Where(r => r.UserSender.Id == user.Id && r.RequestType == RequestType.CONFIRMED)
                 .Select(r => r.UserReceiver)
                 .Union(_repository.GetAll()
                 .Where(r => r.UserReceiver.Id == user.Id && r.RequestType == RequestType.CONFIRMED)
                 .Select(r => r.UserSender));
        }

        public void Remove(long id)
        {
            _repository.Remove(Get(id));
        }

        public void Update(Request request)
        {
            _repository.Update(request);
        }
        public bool IsFriend(User user, User friend)
        {
            return _repository.GetAll().Where(r =>
            (((r.UserReceiverId == user.Id && r.UserSenderId == friend.Id) ||
            (r.UserReceiverId == friend.Id && r.UserSenderId == user.Id)) &&
            (r.RequestType == RequestType.CONFIRMED))).Count() > 0;
        }
        public bool IsFollower(User user, User friend)
        {
            return _repository.GetAll().Where(r =>
            r.UserSenderId == user.Id && r.UserReceiverId == friend.Id && r.RequestType != RequestType.CONFIRMED)
                .Count() > 0;
        }
        public void AddFriend(User user, User friend)
        {
            if (IsFriend(user, friend) || IsFollower(user, friend))
            {
                return;
            }
            //if friend follows you
            else if (IsFollower(friend, user))
            {
                Request request = _repository.GetAll().SingleOrDefault(r =>
                r.UserSenderId == friend.Id && r.UserReceiverId == user.Id && r.RequestType != RequestType.CONFIRMED);
                request.RequestType = RequestType.CONFIRMED;
                _repository.Update(request);
            }
            else //if users dont have relationship
            {
                Request request = new Request
                {
                    UserReceiver = friend,
                    UserSender = user,
                    RequestType = RequestType.WAITING,
                };
                _repository.Add(request);
            }
        }
        public void DeleteFriend(User user, User friend)
        {
            if (IsFriend(user, friend))
            {
                Request request = _repository.GetAll().SingleOrDefault(r =>
                (((r.UserReceiverId == user.Id && r.UserSenderId == friend.Id) ||
                (r.UserReceiverId == friend.Id && r.UserSenderId == user.Id)) &&
                (r.RequestType == RequestType.CONFIRMED)));
                request.RequestType = RequestType.REJECTED;
                request.UserSender = friend;
                request.UserReceiver = user;
                _repository.Update(request);
                return;
            }
            //if user is follower
            else if (IsFollower(user, friend))
            {
                Request request = _repository.GetAll().SingleOrDefault(r =>
                    r.UserSenderId == user.Id && r.UserReceiverId == friend.Id && r.RequestType != RequestType.CONFIRMED);
                _repository.Remove(request);
            }
            else if (IsFollower(friend, user))
            {
                //!TODO May be add Black List
                return;
            }
        }
    }
}
