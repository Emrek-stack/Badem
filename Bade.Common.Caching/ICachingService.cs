using System;

namespace Bade.Common.Caching
{
    public interface ICachingService
    {
        byte[] Get(string cacheKey);
        void Set(string cacheKey, byte[] cacheObj);
        void Set(string cacheKey, byte[] cacheObj, DateTimeOffset expiredate);
        void Delete(string cacheKey);
        void ClearCache(CacheClearQuery criteria);
    }
}