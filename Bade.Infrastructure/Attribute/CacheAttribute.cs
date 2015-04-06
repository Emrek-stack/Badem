namespace Bade.Infrastructure.Attribute
{
    public class CacheAttribute : System.Attribute
    {
        public string CacheKey { get; set; }
        public string Region { get; set; }
        public int CacheMinute { get; set; }
        public int CacheSecond { get; set; }
        public bool IsSlidingCache { get; set; }
    }
}