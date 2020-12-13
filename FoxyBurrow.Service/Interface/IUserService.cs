using FoxyBurrow.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FoxyBurrow.Service.Interface
{
    public interface IUserService
    {
        Task<User> GetAsync(ClaimsPrincipal User);
        Task<User> GetAsync(string Id);
        Task<User> GetAsyncWithPosts(ClaimsPrincipal User);
        Task<User> GetAsyncWithPosts(string Id);
        Task<IEnumerable<User>> FindOtherUsers(ClaimsPrincipal User, string request);
    }
}
