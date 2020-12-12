using FoxyBurrow.Core.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoxyBurrow.Service.Util.Image
{
    public interface IImageService
    {
        void StoreUserImage(User user, IFormFile image);
        void StorePostImage(Post post, IFormFile image);
        string getUserImagePath(User user);
        string getPostImagePath(Post post);
    }
}
