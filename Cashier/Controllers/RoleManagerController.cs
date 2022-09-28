using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;

namespace Cashier.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class RoleManagerController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleManagerController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            var roles = _roleManager.Roles.ToList();

            if (string.IsNullOrEmpty(roleName))
            {
                ModelState.AddModelError("RoleName", "Role name is required");
                return View("Index", roles);
            }

            //validate if role exists
            if (roles.Any(x => x.Name.ToLower() == roleName.ToLower()))
            {
                ModelState.AddModelError("RoleName", "Role already exists");
                return View("Index", roles);
            }

            var result = await _roleManager.CreateAsync(new IdentityRole
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            });
            
            if (result.Succeeded)
            {
                roles = _roleManager.Roles.ToList();
                return View("Index", roles);
            }
            else
            {
                //add error handling
                ModelState.AddModelError("RoleName", String.Join(", ", result.Errors));
                return View("Index", roles);
            }

            return View("Index", roles);
        }

    }
}
