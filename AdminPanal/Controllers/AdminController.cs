using DomainLayer.Identity;
using EcommerceAPIS.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanal.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<Appuser> userManager;
        private readonly SignInManager<Appuser> signInManager;

        public AdminController(UserManager<Appuser> userManager,SignInManager<Appuser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
          var user =await userManager.FindByEmailAsync(loginDto.Email);
            if(user == null && loginDto.Email==null)
            {
                ModelState.AddModelError("Email", "Email is InVaalid");
                return RedirectToAction(nameof(Login));   
            }
            var result=await signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);
            if(!result.Succeeded||!await userManager.IsInRoleAsync(user, "Admin"))
            {
                ModelState.AddModelError(string.Empty,"You Are Not Authorized");
                return RedirectToAction(nameof(Login));
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout( )
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
