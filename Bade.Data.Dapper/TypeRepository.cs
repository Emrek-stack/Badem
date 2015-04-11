

/*
****** Type Data
****** Generation Date: 16.02.2015 03:11:13
*/

using Bade.Data.Contract;
using Bade.Infrastructure;
using Bade.Infrastructure.Helper;

namespace Bade.Data.Dapper
{
    public class TypeRepository : Repository, ITypeRepository
    {

        public TypeRepository(IConnectionFactory connectionFactory) :
            base(connectionFactory, GlobalConfiguration.DefaultConnectionStringName) { }

    }
}
