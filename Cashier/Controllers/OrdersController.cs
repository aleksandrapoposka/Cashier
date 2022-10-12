using Cashier.Models;
using Entities.Orders;
using Entities.User;
using InfrastructureSql.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cashier.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IRepository<Order> _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public OrdersController(ILogger<OrdersController> logger, IRepository<Order> orderRepository, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _userManager = userManager;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] List<OrderDataViewModel> orderData)
        {
            try
            {
                var modifiedBy = User.Identity.Name;
                var modifiedOn = DateTime.UtcNow;
                var newOrder = new Order
                {
                    UserId = _userManager.GetUserId(User),
                    ModifiedBy = modifiedBy,
                    ModifiedOn = DateTime.UtcNow,
                    OrderDetails = orderData.Select(x => new OrderDetails
                    {
                        ArticleId = x.ArticleId,
                        Count = x.Count,
                        Price = x.Price,
                        ModifiedBy = modifiedBy,
                        ModifiedOn = modifiedOn
                    }).ToList()
                };
                await _orderRepository.Add(newOrder);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
