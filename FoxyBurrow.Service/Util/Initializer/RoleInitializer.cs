using FoxyBurrow.Core.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoxyBurrow.Service.Util.Initializer
{
    public class RoleInitializer
    {
        /*private readonly IConfiguration _configuration;
        public RoleInitializer(IConfiguration configuration)
        {
            _configuration = configuration;
        }*/

        public async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //string adminEmail = _configuration["AdminSettings:Mail"];
            //string password = _configuration["AdminSettings:Password"];
            string adminEmail = "afoxyburrow@gmail.com";
            string password = "A1JHoa8M0f";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true};
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
