using FoxyBurrow.Core.Entity;
using FoxyBurrow.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        public ImageService(IHostingEnvironment hostingEnvironment, IUserInformationService userInformationService,
                        IPostService postService, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _userInformationService = userInformationService;
            _postService = postService;
            _configuration = configuration;
        }
        public void StoreUserImage(User user, IFormFile image)
        {
            if (image != null && user?.UserInformation != null)
            {
                var imagePathSection = _configuration.GetSection("ImagePath");
                //string imgFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
                //string avatarFolder = Path.Combine(imgFolder, "avatar");
                string avatarFolder = imagePathSection["AvatarDirectory"];
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(avatarFolder, uniqueFileName);

                image.CopyTo(new FileStream(filePath, FileMode.Create));

                user.UserInformation.ImagePath = uniqueFileName;
                _userInformationService.Update(user.UserInformation);
            }
        }
        public void StorePostImage(Post post, IFormFile image)
        {
            if (image != null && post != null)
            {
                var imagePathSection = _configuration.GetSection("ImagePath");
                //string imgFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
                //string postFolder = Path.Combine(imgFolder, "post");
                string postFolder = imagePathSection["PostImageDirectory"];
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(postFolder, uniqueFileName);

                image.CopyTo(new FileStream(filePath, FileMode.Create));

                post.ImagePath = uniqueFileName;
                _postService.Update(post);
            }
        }
        public string getUserImagePath(User user)
        {
            string filename = user?.UserInformation?.ImagePath;
            var imagePathSection = _configuration.GetSection("ImagePath");
            if (filename != null)
            {
                //return "~/img/avatar/" + filename;
                return imagePathSection["AvatarDirectory"] + filename;
            }
            //return "~/img/avatar/Default.png";
            return imagePathSection["DefaultAvatarPath"];
        }
        public string getPostImagePath(Post post)
        {
            string filename = post?.ImagePath;
            var imagePathSection = _configuration.GetSection("ImagePath");
            if (filename != null)
            {
                //return "~/img/post/" + filename;
                return imagePathSection["PostImageDirectory"] + filename;
            }
            return null;
        }
    }
}
