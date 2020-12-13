using FoxyBurrow.Core.Entity;
using FoxyBurrow.Service.Interface;
using FoxyBurrow.Service.Util.Comparator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRequestService _requestService;

        public UserService(UserManager<User> userManager, IUserInformationService userInformationService,
                        IPostService postService, ICommentService commentService, IRequestService requestService)
        {
            _userManager = userManager;
            _userInformationService = userInformationService;
            _postService = postService;
            _commentService = commentService;
            _requestService = requestService;
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
            List<Post> posts = _postService.GetAllWithComments(user).ToList();
            PostComparer pc = new PostComparer();
            posts.Sort(pc);
            user.Posts = posts;
            return user;
        }
        public async Task<IEnumerable<User>> FindOtherUsers(ClaimsPrincipal User, string request)
        {
            User user = await _userManager.GetUserAsync(User);
            var users = _userManager.Users.Include(u => u.UserInformation).Where(u => u.Id != user.Id).ToList();
            if(string.IsNullOrEmpty(request))
            {
                return users;
            }
            var words = request.Split();
            var cmp = StringComparison.OrdinalIgnoreCase;
            users = (words.Count()) switch
            {
                0 => users,
                1 => users.Where(u => u.UserInformation.FirstName.StartsWith(request, cmp) 
                                    || u.UserInformation.SecondName.StartsWith(request, cmp)).ToList(),
                2 => users.Where(u => u.UserInformation.FirstName.StartsWith(words[0], cmp)
                                    && u.UserInformation.SecondName.StartsWith(words[1], cmp)
                                    || u.UserInformation.FirstName.StartsWith(words[1], cmp)
                                    && u.UserInformation.SecondName.StartsWith(words[0], cmp)
                                    ).ToList(),
                _ => null,
            };
            return users;
        }
        public ICollection<User> GetFriends(User user)
        {
            ICollection<User> friends = _requestService.GetUserFriends(user).ToList();
            foreach(var friend in friends)
            {
                friend.UserInformation = _userInformationService.Get(friend);
            }
            return friends;
        }
        public ICollection<User> GetFollowers(User user)
        {
            ICollection<User> followers = _requestService.GetAllIncomingByUser(user).ToList();
            foreach (var follower in followers)
            {
                follower.UserInformation = _userInformationService.Get(follower);
            }
            return followers;
        }
        public ICollection<User> GetFollowing(User user)
        {
            ICollection<User> following = _requestService.GetAllOutgoingByUser(user).ToList();
            foreach (var follow in following)
            {
                follow.UserInformation = _userInformationService.Get(follow);
            }
            return following;
        }
    }
}
