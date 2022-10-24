using Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureStorageAccount.Interfaces
{
    public interface IQueueRepository
    {
        Task SendOrder(Order order);
    }
}
