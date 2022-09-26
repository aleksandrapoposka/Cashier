using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cashier.Controllers
{
    [Authorize]
    public class RoleManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
