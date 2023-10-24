using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Traversal.Areas.Admin.Models;

namespace Traversal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var values=_roleManager.Roles.ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRolVM createRolVM)
        {
            AppRole role = new AppRole()
            {
                Name = createRolVM.RoleName
            };
            var result = await _roleManager.CreateAsync(role);
            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> DeleteRole(int id)
        {
            var value=_roleManager.Roles.FirstOrDefault(x => x.Id == id);
            await _roleManager.DeleteAsync(value);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UpdateRole(int id)
        {
            var value = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            UpdateRoleVM updateRoleVM = new UpdateRoleVM()
            {
                RoleID = value.Id,
                RoleName = value.Name
            };
            return View(updateRoleVM);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleVM updateRoleVM)
        {
            var value = _roleManager.Roles.FirstOrDefault(x => x.Id == updateRoleVM.RoleID);
            value.Name = updateRoleVM.RoleName;
            await _roleManager.UpdateAsync(value);
            return RedirectToAction("Index");
        }
        public IActionResult UserList()
        {
            var values = _userManager.Users.ToList();
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> AssignRole(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);
            List<AssignRoleVM> assignRoleVMs = new List<AssignRoleVM>();
            foreach (var role in roles)
            {
                AssignRoleVM model = new AssignRoleVM();
                model.RoleId=role.Id;
                model.RoleName = role.Name;
                model.RoleExist = userRoles.Contains(role.Name);
                assignRoleVMs.Add(model);
            }
            return View(assignRoleVMs);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(List<AssignRoleVM> model)
        {
            var userid = (int)TempData["userid"];
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userid);
            foreach(var item in model)
            {
                if (item.RoleExist)
                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }
            return RedirectToAction("UserList");
        }
    }
}
