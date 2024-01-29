using AdminPanal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StackExchange.Redis;

namespace AdminPanal.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
           _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles=await _roleManager.Roles.ToListAsync();   
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleFormViewModel roleForm)
        {
           if(ModelState.IsValid)
            {
                var RoleExist = await _roleManager.RoleExistsAsync(roleForm.Name);
                if(!RoleExist)
                {
                  await _roleManager.CreateAsync(new IdentityRole (roleForm.Name.Trim()));
                    return RedirectToAction(nameof(Index));
                }
               else
                {
                    ModelState.AddModelError("Name", "Role Is Exists");
                    return View("Index",await _roleManager.Roles.ToListAsync());
                }
            }
           return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(string id)
        {
            var role= await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(role);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var mappedRole = new RoleViewModel()
            {
                Name = role.Name,
            };
            return View(mappedRole);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model,string id)
        {
            if (ModelState.IsValid)
            {
                var RoleExist = await _roleManager.RoleExistsAsync(model.Name);
                if (!RoleExist)
                {
                  var role=await _roleManager.FindByIdAsync(model.Id);
                   role.Name = model.Name;
                    await _roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Name", "Role Is Exists");
                    return View("Index", await _roleManager.Roles.ToListAsync());
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
