namespace MemoryCacheProvider
{
    public interface IMemoryCacheProvider
    {
        T Get<T>(string key);
        void Set(string key, object value, int? expirationInMinutes = null);
    }
}