#region

using Bade.Data.Contract;
using Bade.Infrastructure;
using Bade.Infrastructure.Helper;

#endregion

namespace Bade.Data.Dapper
{
    public class MemberRepository : Repository, IMemberRepository
    {      
        public MemberRepository(IConnectionFactory connectionFactory)
            : base(connectionFactory, GlobalConfiguration.DefaultConnectionStringName)
        {
           
        }

    }
}