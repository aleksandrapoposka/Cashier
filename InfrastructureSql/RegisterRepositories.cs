using Entities;
using Entities.Articles;
using Entities.Orders;
using InfrastructureSql.Concrete;
using InfrastructureSql.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace InfrastructureSql
{
    public static class RegisterRepositories
    {
        public static IServiceCollection RegisterSqlRepositories(this IServiceCollection services)
        {
            services.TryAddEnumerable(new[]
            {
                ServiceDescriptor.Scoped<IRepository<Article>, ArticleRepository>(),
                ServiceDescriptor.Scoped<IRepository<Country>, CountryRepository>(),
                ServiceDescriptor.Scoped<IRepository<Order>, OrderRepository>(),
                ServiceDescriptor.Scoped<IReportRepository, ReportRepository>()
            });

            return services;
        }
    }
}
