#region

using Bade.Data.Contract;
using Bade.Infrastructure;
using Bade.Infrastructure.Helper;

#endregion

namespace Bade.Data.Dapper
{
    public class RouteRepository : Repository, IRouteRepository
    {


        public RouteRepository(IConnectionFactory connectionFactory)
            : base(connectionFactory, GlobalConfiguration.DefaultConnectionStringName)
        {
            
        }
    }
}