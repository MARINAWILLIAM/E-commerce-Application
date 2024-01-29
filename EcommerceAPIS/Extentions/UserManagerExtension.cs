using DomainLayer.Identity;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace EcommerceAPIS.Extentions
{
    public static class UserManagerExtension
    {
        public static async Task<Appuser?> FindUsersWithAddress(this UserManager<Appuser> userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);//email
            var user=await userManager.Users.Include(u=>u.Address).FirstOrDefaultAsync(u=>u.Email==email);
           //user nafso
            
            return user;
        }
         
        //users prop feha kol users
    }
}
