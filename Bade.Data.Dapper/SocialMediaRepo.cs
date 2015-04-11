#region

using Bade.Data.Contract;
using Bade.Infrastructure;
using Bade.Infrastructure.Helper;

#endregion

namespace Bade.Data.Dapper
{
    public class SocialMediaRepository : Repository, ISocialMediaRepository
    {
       
        public SocialMediaRepository(IConnectionFactory connectionFactory)
            : base(connectionFactory, GlobalConfiguration.DefaultConnectionStringName)
        {
            
        }

    }
}