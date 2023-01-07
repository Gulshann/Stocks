using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Services.Cache
{
    public class LocalCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public LocalCacheService(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        public T Get<T>(string key)
        {
            bool isCacheKeyFound = _memoryCache.TryGetValue<T>(key, out T cacheValue);

            if (isCacheKeyFound)
            {
                return cacheValue;
            }

            return default;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            bool isCacheKeyFound = _memoryCache.TryGetValue<T>(key, out T cacheValue);

            if (isCacheKeyFound)
            {
                return cacheValue;
            }

            await Task.FromResult(0);

            return default;
        }

        public void Insert(string key, object item, int expirationMinutes)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(string key, object item, int expirationMinutes)
        {
            var expiryTimeSpan = TimeSpan.FromMinutes(expirationMinutes);

            _memoryCache.Set(key, item, absoluteExpirationRelativeToNow: expiryTimeSpan);

            await Task.FromResult(0);
        }

        public async Task RemoveAsync(string key)
        {
            await Task.FromResult(0);

            _memoryCache.Remove(key);

        }
    }
}
