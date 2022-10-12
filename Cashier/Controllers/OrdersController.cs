using Entities.Orders;
using InfrastructureSql.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cashier.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IRepository<Order> _orderRepository;
        
        public OrdersController(ILogger<OrdersController> logger, IRepository<Order> orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddOrder()
        {
            return View();
        }
    }
}
