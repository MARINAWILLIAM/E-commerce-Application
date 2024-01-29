using DomainLayer.Identity;
using Microsoft.AspNetCore.Identity;
using RepositoryLayer.DataBaseHandler.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Identity
{
    public static class AppidentityDataSeeding
    {
        public static async Task seedusersasync(UserManager<Appuser> userManager)
        {
            if (!userManager.Users.Any() )
            {
                //create user eli hn3malo seeding
                var user = new Appuser()
                {
                    DisplayName = "Mariam Magdy",
                    Email = "MariamMagdy@gmail.com",
                    UserName ="Mariam.Magdy",
                    PhoneNumber = "1234567890"
                };
                await userManager.CreateAsync(user,"Pa$$w0rd"); 
               //seeding

            }
        }
    }
}
