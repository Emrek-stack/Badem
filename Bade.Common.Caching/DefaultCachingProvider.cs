using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bade.Infrastructure.Serialization;

namespace Bade.Common.Caching
{
    public class DefaultCachingProvider : ICachingProvider
    {
        private readonly Dictionary<int, ICachingService> _services;
        private readonly IBinarySerializer _serializer;

        public DefaultCachingProvider(Dictionary<int, ICachingService> services, IBinarySerializer serializer)
        {
            _services = services;
            _serializer = serializer;
        }
        public Task<T> ExecuteCachedAsync<T>(Func<T> cachedItem, string key)
        {
            return Task.Factory.StartNew(() =>
            {
                if (_services.First().Value.Get(key) == null)
                {
                    var result = cachedItem();
                    Task.Factory.StartNew(() =>
                    {
                        foreach (var service in _services)
                        {
                            service.Value.Set(key, _serializer.Serialize(result));
                        }
                    });
                    return result;
                }
                else
                    return _serializer.Deserialize<T>(_services.First().Value.Get(key));
            });
        }
        public T ExecuteCached<T>(Func<T> cachedItem, string key)
        {
            if (_services.First().Value.Get(key) == null)
            {
                var result = cachedItem();
                Task.Factory.StartNew(() =>
                {
                    foreach (var service in _services)
                    {
                        service.Value.Set(key, _serializer.Serialize(result));
                    }
                });
                return result;
            }
            else
                return _serializer.Deserialize<T>(_services.First().Value.Get(key));
        }

    }
}