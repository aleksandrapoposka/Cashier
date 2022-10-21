using InfrastructureStorageAccount.Concrete;
using InfrastructureStorageAccount.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureStorageAccount
{
    public static class RegisterRepositories
    {
        public static IServiceCollection RegisterAzureRepositories(this IServiceCollection services)
        {
            services.TryAddEnumerable(new[]
            {
                ServiceDescriptor.Scoped<IBlobRepository,BlobRepository>(),
                ServiceDescriptor.Scoped<ITableRepository,TableRepository>()
            });

            return services;
        }
    }
}
