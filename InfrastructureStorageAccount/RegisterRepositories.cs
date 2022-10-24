using InfrastructureStorageAccount.Concrete;
using InfrastructureStorageAccount.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace InfrastructureStorageAccount
{
    public static class RegisterRepositories
    {
        public static IServiceCollection RegisterAzureRepositories(this IServiceCollection services)
        {
            services.TryAddEnumerable(new[]
            {
                ServiceDescriptor.Scoped<IBlobRepository,BlobRepository>(),
                ServiceDescriptor.Scoped<ITableRepository,TableRepository>(),
                ServiceDescriptor.Scoped<IQueueRepository,QueueRepository>()
            });

            return services;
        }
    }
}
