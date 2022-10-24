using Azure.Storage.Queues;
using DataAccess.Interfaces;
using Entities.Orders;
using InfrastructureStorageAccount.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace InfrastructureStorageAccount.Concrete
{
    public class QueueRepository : IQueueRepository
    {
        private readonly IStorageAccountConnection _storageAccountConnection;
        private readonly QueueClient _queueClient; 

        public QueueRepository(IStorageAccountConnection storageAccountConnection)
        {
            _storageAccountConnection = storageAccountConnection;
            _queueClient = new QueueClient(_storageAccountConnection.ConnectionString, _storageAccountConnection.Queue);
        }

        public async Task SendOrder(Order order)
        {
            if (_queueClient.Exists())
            {
                string message = JsonConvert.SerializeObject(order,new JsonSerializerSettings() {ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                string base64Order = Convert.ToBase64String(bytes);
                await _queueClient.SendMessageAsync(base64Order);
            }
        }
    }
}
