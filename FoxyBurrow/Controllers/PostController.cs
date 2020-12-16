using FoxyBurrow.Core.Entity;
using FoxyBurrow.Models;
using FoxyBurrow.Service.Interface;
using FoxyBurrow.Service.Util.Image;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoxyBurrow.Controllers
{
    public class PostController : Controller
    {
        private readonly IUserService _userService;
        private readonly IImageService _imageService;
        private readonly IPostService _postService;
        private readonly ILogger<HomeController> _logger;
        public PostController(IUserInformationService userInformationService, IPostService postService,
                            IUserService userService, IImageService imageService, ILogger<HomeController> logger)
        {
            _userService = userService;
            _imageService = imageService;
            _postService = postService;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Create(string userId)
        {
            var model = new PostViewModel
            {
                UserId = userId
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel model)
        {
            if (string.IsNullOrEmpty(model.Text))
            {
                return RedirectToAction("Index", "Profile", new { id = model.UserId });
            }
            User user = await _userService.GetAsync(User);
            Post post = new Post
            {
                Comments = new List<Comment>(),
                Text = model.Text,
                UserId = user.Id,
            };
            _postService.Add(post);
            _imageService.StorePostImage(post, model.Image);
            _postService.Update(post);
            return RedirectToAction("Index","Profile",new { id = model.UserId});
        }
        [HttpGet]
        public IActionResult Edit(long postId)
        {
            Post post = _postService.Get(postId);
            if(post == null)
            {
                return Redirect("Error");
            }
            var model = new PostViewModel
            {
                UserId = post.UserId,
                PostId = postId,
                Text = post.Text,
                ImagePath = post.ImagePath
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(PostViewModel model)
        {
            if (string.IsNullOrEmpty(model.Text))
            {
                return Redirect("Error");
            }
            Post post = _postService.Get(model.PostId);
            if(post == null)
            {
                return Redirect("Error");
            }
            post.Text = model.Text;
            _imageService.StorePostImage(post, model.Image);
            _postService.Update(post);
            return RedirectToAction("Index", "Profile", new { id = model.UserId });
        }
        public async Task<IActionResult> Delete(long id)
        {
            User user = await _userService.GetAsync(User);
            Post post = _postService.Get(id);
            if(post.UserId == user.Id)
            {
                _postService.Remove(id);
            }
            return RedirectToAction("Index", "Profile", new { id = user.Id});
        }
    }
}
