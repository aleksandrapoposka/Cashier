using InfrastructureSql.Concrete;
using InfrastructureSql.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(ProcessOrders.Startup))]

namespace ProcessOrders
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IOrderHistoryRepository, OrderHistoryRepository>();
        }
    }
}
