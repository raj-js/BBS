using System;
using Microsoft.Extensions.Caching.Memory;

namespace EDoc2.FAQ.Cache
{
    public class RedisCache : IMemoryCache
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(object key, out object value)
        {
            throw new NotImplementedException();
        }

        public ICacheEntry CreateEntry(object key)
        {
            throw new NotImplementedException();
        }

        public void Remove(object key)
        {
            throw new NotImplementedException();
        }
    }
}
