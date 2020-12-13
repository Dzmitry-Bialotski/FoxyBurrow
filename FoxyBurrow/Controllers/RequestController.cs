using FoxyBurrow.Core.Entity;
using FoxyBurrow.Models;
using FoxyBurrow.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoxyBurrow.Controllers
{
    public class RequestController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRequestService _requestService;
        public RequestController(IUserService userService, IRequestService requestService)
        {
            _userService = userService;
            _requestService = requestService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> SearchFriends(string request)
        {
            var model = new SearchFriendsViewModel
            {
                Users = (await _userService.FindOtherUsers(User, request)).ToList(),
                Request = request,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SearchFriends(SearchFriendsViewModel model)
        {
            model.Users = (await _userService.FindOtherUsers(User, model?.Request)).ToList();
            return View(model);
        }
    }
}
