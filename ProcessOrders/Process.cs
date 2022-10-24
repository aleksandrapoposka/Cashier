using Entities.Orders;
using InfrastructureSql.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ProcessOrders
{
    public class Process
    {
        private readonly IOrderHistoryRepository _orderHistoryRepository;

        public Process(IOrderHistoryRepository orderHistoryRepository)
        {
            _orderHistoryRepository = orderHistoryRepository;
        }

        [FunctionName("Process")]
        public void Run([QueueTrigger("orders", Connection = "StorageAccount")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processing: {myQueueItem}");

            _orderHistoryRepository.UpdateOrderHistory(JsonConvert.DeserializeObject<Order>(myQueueItem).Id);

            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
