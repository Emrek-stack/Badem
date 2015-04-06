using System;
using System.Threading.Tasks;

namespace Bade.Common.Caching
{
    public interface ICachingProvider
    {
        Task<T> ExecuteCachedAsync<T>(Func<T> cachedItem, string key);
        T ExecuteCached<T>(Func<T> cachedItem, string key);
    }
}