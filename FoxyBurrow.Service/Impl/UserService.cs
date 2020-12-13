using FoxyBurrow.Core.Entity;
using FoxyBurrow.Service.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FoxyBurrow.Service.Impl
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserInformationService _userInformationService;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public UserService(UserManager<User> userManager, IUserInformationService userInformationService,
                        IPostService postService, ICommentService commentService)
        {
            _userManager = userManager;
            _userInformationService = userInformationService;
            _postService = postService;
            _commentService = commentService;
        }

        public async Task<User> GetAsync(ClaimsPrincipal User)
        {
            User user = await _userManager.GetUserAsync(User);
            user.UserInformation = _userInformationService.Get(user);
            return user;
        }

        public async Task<User> GetAsync(string Id)
        {
            User user = await _userManager.FindByIdAsync(Id);
            user.UserInformation = _userInformationService.Get(user);

            return user;
        }

        public async Task<User> GetAsyncWithPosts(string Id)
        {
            User user = await _userManager.FindByIdAsync(Id);
            user.UserInformation = _userInformationService.Get(user);
            user.Posts = _postService.GetAllWithComments(user).ToList();
            return user;
        }
        public async Task<User> GetAsyncWithPosts(ClaimsPrincipal User)
        {
            User user = await _userManager.GetUserAsync(User);
            user.UserInformation = _userInformationService.Get(user);
            user.Posts = _postService.GetAllWithComments(user).ToList();
            return user;
        }
    }
}
