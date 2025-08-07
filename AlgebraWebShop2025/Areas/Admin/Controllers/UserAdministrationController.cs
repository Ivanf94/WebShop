using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlgebraWebShop2025.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class UserAdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserAdministrationController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult RoleIndex()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string rolename)
        {
            if (string.IsNullOrEmpty(rolename))
            {
                ViewBag.Message = "Role name can't be empty!";
                return View();
            }

            bool roleExists = await _roleManager.RoleExistsAsync(rolename);
            if (roleExists)
            {
                ViewBag.Message = "A role with that name already exists! Give the role a new unique name!";
                return View();
            }

            IdentityRole ir = new IdentityRole
            {
                Name = rolename
            };

            IdentityResult result = await _roleManager.CreateAsync(ir);

            if (result.Succeeded)
            {
                return RedirectToAction("RoleIndex");
            }

            string err = "";
            foreach(var error in result.Errors)
            {
                err += error.Description + "\n";
            }
            ViewBag.Message = err;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role=await _roleManager.FindByIdAsync(roleId);
            if(role == null)
            {
                return RedirectToAction(nameof(RoleIndex));
            }

            var result = await _roleManager.DeleteAsync(role);

            return RedirectToAction(nameof(RoleIndex));
        }

        public async Task<IActionResult> EditRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return RedirectToAction(nameof(RoleIndex));
            }

            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(IdentityRole model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                return RedirectToAction(nameof(RoleIndex));
            }
            if (await _roleManager.RoleExistsAsync(model.Name)) return View(model);
            role.Name = model.Name;
            var result = _roleManager.UpdateAsync(role);
            if(result.IsCompletedSuccessfully) return RedirectToAction(nameof(RoleIndex));

            return View(model);
        }
    }
}
