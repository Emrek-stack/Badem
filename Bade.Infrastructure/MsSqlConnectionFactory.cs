using System.Configuration;
using System.Data.SqlClient;

namespace Bade.Infrastructure
{
    public class MsSqlConnectionFactory : IConnectionFactory
    {              
        private string _connectionString;
        
        public SqlConnection Create(string connectionStringName)
        {
            _connectionString = ConfigurationManager.AppSettings[connectionStringName];
            return new SqlConnection(_connectionString);
        }
    }
}