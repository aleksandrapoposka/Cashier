using Cashier.Models;
using Cashier.Models.Home;
using Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;

namespace Cashier.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(
            ILogger<HomeController> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
    

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                //add custom error to model state, to check as alternate
                //if (userViewModel.DateOfBirth > DateTime.UtcNow.AddYears(-18))
                //{
                //    ModelState.AddModelError(nameof(userViewModel.DateOfBirth), "User must be older than 18 years.");
                //    return View(userViewModel);
                //}
                
                //register user
                var newUser = new ApplicationUser
                {
                    UserName = userViewModel.Email,
                    Email = userViewModel.Email,
                    FirstName = userViewModel.FirstName,
                    LastName = userViewModel.LastName,
                    DateOfBirth = userViewModel.DateOfBirth
                };
                var result = await _userManager.CreateAsync(newUser, userViewModel.Password);
                
                if (result.Succeeded)
                {
                    var cashierRole = _roleManager.Roles.First(x => x.Name == "Cashier");
                    await _userManager.AddToRoleAsync(newUser, cashierRole.Name);
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(userViewModel);
                }
            } else
            {               
                return View(userViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                //login user
                var result = await _signInManager
                    .PasswordSignInAsync(
                        userViewModel.Email, 
                        userViewModel.Password, 
                        userViewModel.RememberMe, 
                        false);
                
                if (result.Succeeded)
                {
                    return RedirectToAction("Privacy");
                } else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(userViewModel);
                }
            } 
            else
            {
                return View(userViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}