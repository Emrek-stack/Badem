#region

using System.Collections.Generic;
using Bade.Constants;
using Bade.Data.Contract;
using Bade.Entity.Domain;
using Bade.Infrastructure;

#endregion

namespace Bade.Data.Dapper
{
    public class ApplicationRepository : Repository, IApplicationRepository
    {               
        public ApplicationRepository(IConnectionFactory connectionFactory)
            : base(connectionFactory, Constant.DefaultConnectionStringName)
        {                        
        }
        public ApplicationConfig GetByApplicationIdAndKey(int applicationId, string key)
        {

            ApplicationConfig applicationConfig = GetStoredProcedure<ApplicationConfig>("GetByApplicationIdAndKey", new { applicationId, key });
            return applicationConfig;
        }

        public IEnumerable<ApplicationConfig> ApplicationConfigListById(int id)
        {
            return null; //GetMany(s => s.ApplicationId == id);
        }

    }
}