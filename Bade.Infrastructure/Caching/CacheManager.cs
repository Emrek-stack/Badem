#region

using System;
using System.Runtime.Caching;
using Bade.Lib.Common.Caching;

#endregion

namespace Bade.Infrastructure.Caching
{
    public class CacheManager : ICacheManager
    {
        #region Fields

        private string _cacheKey = string.Empty;

        private static ObjectCache _cache;

        private static ObjectCache Cache
        {
            get { return _cache ?? (_cache = new MemoryCache("FanatikCache")); }
        }

        #endregion

        #region ICacheProvider Members

        public T AddOrGetExistingWithLock<T>(string key, Func<T> valueFactory)
        {
            CacheItemPolicy policy = new CacheItemPolicy {Priority = CacheItemPriority.NotRemovable};

            var newValue = new Lazy<T>(valueFactory);
            var value = (Lazy<T>) Cache.AddOrGetExisting(key, newValue, policy);

            T returnValue = (value ?? newValue).Value; // Lazy<T> handles the locking itself

            return returnValue;
        }

        public T AddOrGetExistingWithLock<T>(string key, Func<T> valueFactory, int minute)
        {
            var newValue = new Lazy<T>(valueFactory);
            var value = (Lazy<T>) Cache.AddOrGetExisting(key, newValue, DateTime.Now.AddMinutes(minute));

            T returnValue = (value ?? newValue).Value; // Lazy<T> handles the locking itself

            return returnValue;
        }

        public T Get<T>(string cacheKey)
        {
            _cacheKey = cacheKey;
            var resultObject = (T) Cache[_cacheKey];
            return resultObject;
        }

        public bool IsSet(string key)
        {
            return (Cache[key] != null);
        }

        public void InValidate(string key)
        {
            Cache.Remove(key);
        }

        public string[] Clear()
        {
            string[] cacheItems = GetAllItems();
            if (Cache == null) return new string[0];
            foreach (var cacheItem in Cache)
            {
                Cache.Remove(cacheItem.Key);
            }
            return cacheItems;
        }

        public string[] GetAllItems()
        {
            string[] cacheItems = new string[Cache.GetCount()];
            int i = 0;

            foreach (var cacheItem in Cache)
            {
                cacheItems[i] = cacheItem.Key;
                i++;
            }

            return cacheItems;
        }

        #endregion
    }
}