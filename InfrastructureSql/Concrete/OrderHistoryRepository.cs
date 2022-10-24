using InfrastructureSql.Interfaces;
using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace InfrastructureSql.Concrete
{
    public class OrderHistoryRepository : IOrderHistoryRepository
    {
        private IDbConnection database;

        public OrderHistoryRepository(IConfiguration configuration)
        {
            database = new SqlConnection(configuration["SqlConnection"]);
        }

        public async Task UpdateOrderHistory(long id)
        {
            await database.ExecuteAsync("UPDATE OrderDetailsHistory SET Operation = 'Processed' WHERE OrderId = @OrderId",new { OrderId = id });
        }
    }
}
