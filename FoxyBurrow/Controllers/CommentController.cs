using FoxyBurrow.Core.Entity;
using FoxyBurrow.Models;
using FoxyBurrow.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoxyBurrow.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private readonly ILogger<HomeController> _logger;
        public CommentController(ICommentService commentService, IUserService userService,
                                ILogger<HomeController> logger)
        {
            _commentService = commentService;
            _userService = userService;
            _logger = logger;
        }
        public async Task<IActionResult> AddComment(ProfileViewModel model)
        {
            if(string.IsNullOrEmpty(model.Text))
            {
                return RedirectToAction("Index", "Profile", new { id = model.UserToReturnId });
            }
            User curUser = await _userService.GetAsync(User);
            Comment comment = new Comment()
            {
                Text = model.Text,
                PostId = model.PostId, 
                UserId = curUser.Id
            };
            _commentService.Update(comment);
            return RedirectToAction("Index", "Profile", new { id = model.UserToReturnId });
        }
        public IActionResult DeleteComment(ProfileViewModel model)
        {
            _commentService.Remove(model.CommentId);
            return RedirectToAction("Index", "Profile", new { id = model.UserToReturnId});
        }
    }
}
