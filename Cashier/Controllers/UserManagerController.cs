using Cashier.Models.UserManager;
using Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cashier.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class UserManagerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserManagerController(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var allUsersViewModel = new List<UserRolesViewModel>();
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var userRolesModel = new UserRolesViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    UserRoles = new List<string>(userRoles)
                };
                allUsersViewModel.Add(userRolesModel);
            }
            //var roles = _roleManager.Roles.ToList();
            return View(allUsersViewModel);
        }
        
        public async Task<IActionResult> Manage(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                //return error
            }
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null) 
            { 
                //return error
            }
            var allRolesViewModel = new List<ManageUserRolesViewModel>();
            var allRoles = _roleManager.Roles.ToList();
            foreach (var role in allRoles)
            {
                var roleViewModel = new ManageUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    IsSelected = await _userManager.IsInRoleAsync(user, role.Name)
                };
                allRolesViewModel.Add(roleViewModel);
            }
            ViewBag.UserId = userId;
            ViewBag.Username = user.UserName;

            return View(allRolesViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> userRoles, 
            string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                //return error
            }
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                //return error
            }
            //remove user roles
            var currentUserRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(
                user,
                currentUserRoles);

            //add user to selected roles
            await _userManager.AddToRolesAsync(
                user,
                userRoles.Where(x => x.IsSelected).Select(x => x.RoleName).ToList());

            return RedirectToAction("Index");
        }
    }
}
