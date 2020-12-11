using FoxyBurrow.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoxyBurrow.Service.Interface
{
    public interface IUserInformationService
    {
        UserInformation Get(long id);
        UserInformation Get(User user);
        IQueryable<UserInformation> GetAll();
        void Add(UserInformation userInformation);
        void Update(UserInformation userInformation);
        void Remove(long id);
    }
}
