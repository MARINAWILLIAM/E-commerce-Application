using AdminPanal.Models;
using DomainLayer.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace AdminPanal.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<Appuser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController( UserManager<Appuser> userManager,RoleManager<IdentityRole> roleManager)
        {
           _userManager = userManager;
          _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var users=await _userManager.Users.Select(u=>new UserViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                DisplayName = u.DisplayName,
                Roles=_userManager.GetRolesAsync(u).Result,
            }).ToListAsync();
            return View(users );
        }
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var AllRoles = await _roleManager.Roles.ToListAsync();
            var ViewModel = new UserRolesViewModel
            {
             UserId = user.Id,
             UserName= user.DisplayName,
             Roles=AllRoles.Select(r => new RoleViewModel
             {
                 Id = r.Id,
                 Name = r.Name,
                  IsSelected = _userManager.IsInRoleAsync(user,r.Name).Result,
             }).ToList(),

            };
            return View(ViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var UserRoles = await _userManager.GetRolesAsync(user);
          foreach (var role in model.Roles)
            {
                if(UserRoles.Any(r=>r==role.Name)&&!role.IsSelected) {
                    //REMOVE THIS ROLE
                    await _userManager.RemoveFromRoleAsync(user,role.Name);
                }
                if (!UserRoles.Any(r => r == role.Name) && role.IsSelected)
                {
                    //ADD THIS ROLE
                await _userManager.AddToRoleAsync(user,role.Name);
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
