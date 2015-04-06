namespace Bade.Common.Caching
{
    public class CacheClearQuery
    {
        public CacheClearQuery(string pattern)
        {
            Pattern = pattern;
        }
        public string Pattern { get; private set; }
    }
}