using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCacheProvider
{
    public class MemoryCacheProvider : IMemoryCacheProvider
    {
        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, object value, int? expirationInMinutes = null)
        {
            throw new NotImplementedException();
        }
    }
}
