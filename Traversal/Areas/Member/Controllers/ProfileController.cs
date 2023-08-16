using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Traversal.Areas.Member.Models;

namespace Traversal.Areas.Member.Controllers
{
    [Area("Member")]
    [Route("Member/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditVM userEditVM=new UserEditVM();
            userEditVM.name=values.Name;
            userEditVM.surname = values.Surname;
            userEditVM.mail = values.Email;
            userEditVM.phonenumber = values.PhoneNumber;
            return View(userEditVM);
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserEditVM p)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (p.image != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(p.image.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/userimages/" + imagename;
                var stream=new FileStream(savelocation, FileMode.Create);
                await p.image.CopyToAsync(stream);
                user.ImageUrl = "/userimages/" + imagename;
            }
            user.Name=p.name;
            user.Surname = p.surname;
            user.PhoneNumber = p.phonenumber;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user,p.password);
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Signin", "User");
            }
            return View();
        }
    }
}
