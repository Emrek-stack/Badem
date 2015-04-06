

/*
****** Status Data
****** Generation Date: 16.02.2015 03:11:13
*/

using Bade.Constants;
using Bade.Data.Contract;
using Bade.Entity.Domain;
using Bade.Infrastructure;

namespace Bade.Data.Dapper
{
    public class StatusRepository : Repository, IStatusRepository
    {

        public StatusRepository(IConnectionFactory connectionFactory) :
            base(connectionFactory, Constant.DefaultConnectionStringName)
        {

        }

    }
}
