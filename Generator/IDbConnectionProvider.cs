using System.Data;

namespace Generator
{
    public interface IDbConnectionProvider
    {
        IDbConnection GetDbConnection();
    }
}
