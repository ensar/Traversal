using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Traversal.Models;

namespace Traversal.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

		public UserController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		[HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(UserRegisterVM p)
        {
            AppUser appUser = new AppUser()
            {
                Name = p.Name,
                Surname = p.Surname,
                Email = p.Mail,
                UserName = p.Username
            };
            if (p.Password == p.ConfirmPassword)
            {
                var result = await _userManager.CreateAsync(appUser, p.Password);
                if(result.Succeeded)
                {
                    return RedirectToAction("Signin");
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }
    }
}
