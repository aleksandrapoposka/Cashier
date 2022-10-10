using Newtonsoft.Json;
using System.Threading.Tasks;

namespace InfrastructureRedis
{
    public interface ICacheProvider
    {
        Task<T> GetAsync<T>(string key) where T : class;
        Task SetAsync<T>(string key, T data, int expiresInMinutes, JsonSerializerSettings settings = null) where T : class;
        Task DeleteAsync(string key);
    }
}