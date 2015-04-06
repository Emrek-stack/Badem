#region

using Bade.Admin.Model.Model.Output;
using Bade.Entity.Domain;
using Bade.Infrastructure.Attribute;

#endregion



namespace Bade.Manager.Interface
{
    public interface IApplicationManager : IManager
    {
        [Cache(CacheMinute = 20)]
        ApplicationConfig GetKey(int applicationId, string key);
        ApplicationConfigOutput ApplicationConfigListById(int id);
    }
}