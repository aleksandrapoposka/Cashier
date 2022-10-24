using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureSql.Interfaces
{
    public interface IOrderHistoryRepository
    {
        Task UpdateOrderHistory(long id);
    }
}
