﻿using Cashier.Models.UserManager;
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
    }
}
