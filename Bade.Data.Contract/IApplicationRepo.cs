#region

using System.Collections.Generic;
using Bade.Entity.Domain;

#endregion



namespace Bade.Data.Contract
{
    public interface IApplicationRepository : IRepository
    {
        ApplicationConfig GetByApplicationIdAndKey(int applicationId, string key);
        IEnumerable<ApplicationConfig> ApplicationConfigListById(int id);
    }
}