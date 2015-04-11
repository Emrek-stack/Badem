#region

using Bade.Data.Contract;
using Bade.Infrastructure;
using Bade.Infrastructure.Helper;

#endregion

namespace Bade.Data.Dapper
{
    public class ContentRepository : Repository, IContentRepository
    {
       
        public ContentRepository(IConnectionFactory connectionFactory)
            : base(connectionFactory, GlobalConfiguration.DefaultConnectionStringName)
        {
            
        }
    }
}