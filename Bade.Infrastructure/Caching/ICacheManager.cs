#region

using System;

#endregion

namespace Bade.Lib.Common.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string cacheKey);

        bool IsSet(string key);

        void InValidate(string key);

        T AddOrGetExistingWithLock<T>(string key, Func<T> valueFactory);

        T AddOrGetExistingWithLock<T>(string key, Func<T> valueFactory, int minute);
    }
}