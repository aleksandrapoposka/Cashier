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

        public HomeController(
            ILogger<HomeController> logger,
            UserManager<ApplicationUser> userManager
            )
        {
            _logger = logger;
            _userManager = userManager;
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
        public async Task<IActionResult> Register(ApplicationUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                //    if (userViewModel.DateOfBirth > DateTime.UtcNow.AddYears(-18))
                //    {
                //        userViewModel.Validate(ModelState);

                //    }
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
                return RedirectToAction("Login", "Home");
            } else
            {
                //foreach (var modelState in ModelState.Values)
                //{
                //    foreach (ModelError error in modelState.Errors)
                //    {
                //        userViewModel.
                //    }
                //}
                return View(userViewModel);
            }
        }
    }
}