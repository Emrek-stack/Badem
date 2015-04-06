using System;
using System.Threading.Tasks;
using Bade.Infrastructure.Serialization;

namespace Bade.Common.Caching
{
    public class SingleCachingProvider : ICachingProvider
    {
        private readonly ICachingService _cachingService;
        private readonly IBinarySerializer _serializer;

        public SingleCachingProvider(ICachingService cachingService, IBinarySerializer serializer)
        {
            _cachingService = cachingService;
            _serializer = serializer;
        }

        public Task<T> ExecuteCachedAsync<T>(Func<T> cachedItem, string key)
        {
            return Task.Factory.StartNew(() =>
            {
                if (_cachingService.Get(key) == null)
                {
                    var result = cachedItem();
                    //TODO: will be decided how to define methods for wider usage with Emre
                    _cachingService.Set(key, _serializer.Serialize(result));
                    return result;
                }
                else
                    return _serializer.Deserialize<T>(_cachingService.Get(key));
            });
        }
        public T ExecuteCached<T>(Func<T> cachedItem, string key)
        {
            if (_cachingService.Get(key) != null) return _serializer.Deserialize<T>(_cachingService.Get(key));
            var result = cachedItem();
            _cachingService.Set(key, _serializer.Serialize(result));
            return result;
        }
    }
}