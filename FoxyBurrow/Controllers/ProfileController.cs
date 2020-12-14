using FoxyBurrow.Core.Entity;
using FoxyBurrow.Models;
using FoxyBurrow.Service.Interface;
using FoxyBurrow.Service.Util.Comparator;
using FoxyBurrow.Service.Util.Image;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoxyBurrow.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserInformationService _userInformationService;
        private readonly IUserService _userService;
        private readonly IImageService _imageService;
        private readonly ILogger<HomeController> _logger;
        public ProfileController(IUserInformationService userInformationService,
                            IUserService userService, IImageService imageService, ILogger<HomeController> logger)
        {
            _userInformationService = userInformationService;
            _userService = userService;
            _imageService = imageService;
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync(string id)
        {
            User currentUser = await _userService.GetAsync(User);
            User user = id != null ? await _userService.GetAsyncWithPosts(id) 
                                   : await _userService.GetAsyncWithPosts(User);
            PostComparer pc = new PostComparer();
            user.Posts.Sort(pc);
            ProfileViewModel model = new ProfileViewModel
            {
                User = user,
                currentUserId = currentUser.Id
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditProfile(string id)
        {
            User user = await _userService.GetAsync(User);
            if(user.Id == id)
            {
                UserInformation uinfo = user.UserInformation;
                var model = new ProfileEditViewModel
                {
                    Id = user.Id,
                    FirstName = uinfo.FirstName,
                    SecondName = uinfo.SecondName,
                    ImagePath = uinfo.ImagePath,
                    Birthday = uinfo.Birthday,
                    City = uinfo.City,
                    Gender = uinfo.Gender,
                    PlaceOfStudy = uinfo.PlaceOfStudy,
                    PlaceOfWork = uinfo.PlaceOfWork,
                    AboutMyself = uinfo.AboutMyself,
                    Status = uinfo.Status
                };
                return View(model);
            }
            return View("Error");
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(ProfileEditViewModel model)
        {
            User user = await _userService.GetAsync(model.Id);
            UserInformation uinfo = user.UserInformation;
            uinfo.FirstName = model.FirstName;
            uinfo.SecondName = model.SecondName;
            uinfo.Birthday = model.Birthday;
            uinfo.City = model.City;
            uinfo.Gender = model.Gender;
            uinfo.PlaceOfStudy = model.PlaceOfStudy;
            uinfo.PlaceOfWork = model.PlaceOfWork;
            uinfo.AboutMyself = model.AboutMyself;
            uinfo.Status = model.Status;
            _imageService.StoreUserImage(user, model.Image);
            _userInformationService.Update(uinfo);
            return RedirectToAction("Index","Profile",new { id = model.Id});
        }
    }
}
