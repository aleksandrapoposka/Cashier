using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cashier.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class ArticlesController : Controller
    {
        private readonly ILogger<ArticlesController> _logger;
        
        public ArticlesController(ILogger<ArticlesController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            _logger.LogInformation("ArticlesController.Index");
            return View();
        }
    }
}
