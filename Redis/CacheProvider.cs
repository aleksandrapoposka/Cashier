using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace InfrastructureRedis
{
    public class CacheProvider : ICacheProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IDatabase _cacheProvider;

        public CacheProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            _ = new RedisConnectionFactory(_configuration);
            _cacheProvider = RedisConnectionFactory.Instance.GetDatabase();
        }

        public async Task<T> GetAsync<T>(string key) where T : class
        {
            RedisValue value = await _cacheProvider.StringGetAsync(key, CommandFlags.PreferReplica);
            return value.HasValue ? JsonConvert.DeserializeObject<T>(value) : default(T);
        }

        public async Task SetAsync<T>(string key, T data, int expiresInMinutes, JsonSerializerSettings settings = null) where T : class
        {
            string value = JsonConvert.SerializeObject(data, settings);
            await _cacheProvider.StringSetAsync(key, value, TimeSpan.FromMinutes(expiresInMinutes));
        }

        public async Task DeleteAsync(string key)
        {
            await _cacheProvider.KeyDeleteAsync(key);
        }

    }
}
