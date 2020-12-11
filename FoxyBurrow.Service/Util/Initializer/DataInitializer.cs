using FoxyBurrow.Core.Entity;
using FoxyBurrow.Core.Enum;
using FoxyBurrow.Database.DbContext;
using FoxyBurrow.Service.Impl;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoxyBurrow.Service.Util.Initializer
{
    public class DataInitializer
    {
        public async Task InitData(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if((await userManager.FindByEmailAsync("dimasiandro@gmail.com")) != null)
            {
                return;
            }
            var comments = new List<Comment>
            {
                new Comment { Text = "first comment" },
                new Comment { Text = "second comment" }
            };
            var posts = new List<Post> 
            {
                new Post {  Text = "First Post of Dimasik", Comments = comments },
                new Post {  Text = "Second Post of Dimasik"},
            };
            UserInformation uinfo1 = new UserInformation()
            {
                FirstName = "Dmitriy",
                SecondName = "Belotskiy",
                Birthday = new DateTime(2001, 04, 26),
                City = "Brest",
                Gender = Gender.MALE,
                PlaceOfStudy = "BSUIR",
                Status = "I love ASP.NET Core! <3",
            };
            UserInformation uinfo2 = new UserInformation()
            {
                FirstName = "Alex",
                SecondName = "Boghdan",
                Birthday = new DateTime(2001, 04, 26),
                City = "Brest",
                Gender = Gender.MALE,
                PlaceOfStudy = "Будущая киберкотлета",
                Status = "Жмурика, я ультраслабый",
            };
            User user1 = new User()
            {
                Email = "dimasiandro@gmail.com",
                UserName = "dimasiandro@gmail.com",
                UserInformation = uinfo1,
                Posts = posts
            };
            User user2 = new User()
            {
                Email = "alex@gmail.com",
                UserName = "alex@gmail.com",
                UserInformation = uinfo2,
                Comments = comments
            };
            var result = await userManager.CreateAsync(user1, "Password");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user1, "user");
            }
            result = await userManager.CreateAsync(user2, "Password");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user2, "user");
            }
        }
    }
}
