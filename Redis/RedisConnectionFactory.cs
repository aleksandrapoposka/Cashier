using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRedis
{
    internal sealed class RedisConnectionFactory
    {
        private static Lazy<ConnectionMultiplexer> Connection = null;

        public static ConnectionMultiplexer Instance
        {
            get
            {
                return Connection.Value;
            }
        }

        public RedisConnectionFactory(IConfiguration configuration)
        {
            ConfigurationOptions options = GetConfigurationOptionsFromConnectionString(configuration["ConnectionString:RedisCacheConnectionString"]);
            Connection = new Lazy<ConnectionMultiplexer>(() => { return ConnectionMultiplexer.Connect(options); }, true);
        }

        private static ConfigurationOptions GetConfigurationOptionsFromConnectionString(string connectionString)
        {
            if (connectionString == null) throw new Exception("Redis cache connection string was not found.");

            ConfigurationOptions options = ConfigurationOptions.Parse(connectionString);


            return options;
        }
    }
}
