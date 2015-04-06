#region

using System.Collections.Generic;
using System.Linq;
using Bade.Constants.Enum;

#endregion

namespace Bade.Infrastructure.Caching
{
    public class CacheKeyCompute
    {
        public static string ComputeCacheKey(string objectName, CacheKeyType cacheKeyType, params KeyValuePair<string, string>[] parameters)
        {
            string cacheKey = string.Empty;

            switch (cacheKeyType)
            {
                case CacheKeyType.BusinessCache:

                    cacheKey = string.Format("BusinessData--method[method={0}]", objectName);
                    if (parameters != null)
                    {
                        cacheKey = parameters.Aggregate(cacheKey, (current, route) => current + string.Format(":[{0},{1}]:", route.Key, route.Value));
                    }
                    break;
            }

            return cacheKey;
        }
    }
}