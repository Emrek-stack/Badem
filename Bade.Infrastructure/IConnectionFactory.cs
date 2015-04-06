using System.Data.SqlClient;

namespace Bade.Infrastructure
{
    public interface IConnectionFactory
    {
        SqlConnection Create(string connectionStringName);
    }
}