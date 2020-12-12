using FoxyBurrow.Core.Entity;
using FoxyBurrow.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FoxyBurrow.Service.Util.Image
{ 
    public class ImageService : IImageService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserInformationService _userInformationService;
        private readonly IPostService _postService;
        public ImageService(IHostingEnvironment hostingEnvironment, IUserInformationService userInformationService,
                        IPostService postService)
        {
            _hostingEnvironment = hostingEnvironment;
            _userInformationService = userInformationService;
            _postService = postService;
        }
        public void StoreUserImage(User user, IFormFile image)
        {
            if (image != null && user?.UserInformation != null)
            {
                string avatarFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
                string imgFolder = Path.Combine(avatarFolder, "avatar");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(imgFolder, uniqueFileName);

                image.CopyTo(new FileStream(filePath, FileMode.Create));

                user.UserInformation.ImagePath = uniqueFileName;
                _userInformationService.Update(user.UserInformation);
            }
        }
        public void StorePostImage(Post post, IFormFile image)
        {
            if (image != null && post != null)
            {
                string avatarFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
                string imgFolder = Path.Combine(avatarFolder, "post");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(imgFolder, uniqueFileName);

                image.CopyTo(new FileStream(filePath, FileMode.Create));

                post.ImagePath = uniqueFileName;
                _postService.Update(post);
            }
        }

    }
}
