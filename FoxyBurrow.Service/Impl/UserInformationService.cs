using FoxyBurrow.Core.Entity;
using FoxyBurrow.Database.Repositpry;
using FoxyBurrow.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoxyBurrow.Service.Impl
{
    public class UserInformationService : IUserInformationService
    {
        private readonly IRepository<UserInformation> _repository;

        public UserInformationService(IRepository<UserInformation> repository)
        {
            _repository = repository;
        }
        public void Add(UserInformation userInformation)
        {
            _repository.Add(userInformation);
        }

        public UserInformation Get(long id)
        {
            return _repository.Get(id);
        }

        public UserInformation Get(User user)
        {
            return _repository.GetAll().SingleOrDefault(ui => ui.UserId == user.Id);
        }

        public IQueryable<UserInformation> GetAll()
        {
            return _repository.GetAll();
        }

        public void Remove(long id)
        {
            _repository.Remove(Get(id));
        }

        public void Update(UserInformation userInformation)
        {
            _repository.Update(userInformation);
        }
    }
}
