using Cashier.Models.Orders;
using Entities.Orders;
using Entities.User;
using InfrastructureSql.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Linq;

namespace Cashier.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IRepository<Order> _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IReportRepository _reportRepository;
        public OrdersController(ILogger<OrdersController> logger, 
            IRepository<Order> orderRepository,
            UserManager<ApplicationUser> userManager,
            IReportRepository reportRepository) 
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _userManager = userManager;
            _reportRepository = reportRepository;
        }

        
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            
            var allOrders = await _orderRepository.GetAll() ?? new List<Order>();
            var ordersPerUser = allOrders.Where(x => x.UserId == userId).ToList();
            var ordersViewModel = ordersPerUser.Select(x => new OrderViewModel
            {
                Id = x.Id,
                Total = x.OrderDetails.Sum(y => y.Count * y.Price),
                Description = String.Concat(x.OrderDetails.Select(z => z.Article.Name + ", " + z.Count + "x" + z.Price + " ")),
            });
            return View(ordersViewModel);
        }

        public async Task<IActionResult> AddOrder()
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
