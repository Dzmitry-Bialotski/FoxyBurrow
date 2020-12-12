using FoxyBurrow.Core.Entity;
using FoxyBurrow.Models;
using FoxyBurrow.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoxyBurrow.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserInformationService _userInformationService;
        public ProfileController(UserManager<User> userManager, IUserInformationService userInformationService)
        {
            _userManager = userManager;
            _userInformationService = userInformationService;
        }
        public async Task<IActionResult> IndexAsync(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            user.UserInformation = _userInformationService.Get(user);
            ProfileViewModel model = new ProfileViewModel
            {
                User = user
            };
            return View(model);
        }
    }
}
