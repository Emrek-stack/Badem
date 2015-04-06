using System;
using System.Linq;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace Bade.Common.Caching.InMemory
{
    public class MemoryCacheService : ICachingService
    {
        //TODO: Burasi Emre Karahan tarafindan kontrol edilecek.

        public MemoryCache Cache;

        public MemoryCacheService()
        {
            Cache = MemoryCache.Default;
        }

        public byte[] Get(string cacheKey)
        {
            return (byte[])Cache.Get(cacheKey);
        }

        public void Set(string cacheKey, byte[] cacheObj)
        {
            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.MaxValue,
                Priority = CacheItemPriority.Default
            };
            Cache.Add(new CacheItem(cacheKey, cacheObj), policy);
        }

        public void Set(string cacheKey, byte[] cacheObj, DateTimeOffset expiredate)
        {
            var policy = new CacheItemPolicy {AbsoluteExpiration = expiredate, Priority = CacheItemPriority.Default};
            Cache.Add(new CacheItem(cacheKey, cacheObj), policy);
        }

        public void Delete(string cacheKey)
        {
            Cache.Remove(cacheKey);
        }

        public void ClearCache(CacheClearQuery criteria)
        {
            var keyList = Cache.Select(f => f.Key);
            if (criteria != null && !string.IsNullOrEmpty(criteria.Pattern))
            {
                var r = new Regex(criteria.Pattern, RegexOptions.None, new TimeSpan(0, 0, 1));
                keyList = keyList.Where(x => r.IsMatch(x));
            }
            keyList = keyList.ToArray();
            foreach (string key in keyList)
            {
                Cache.Remove(key);
            }
        }
    }
}
