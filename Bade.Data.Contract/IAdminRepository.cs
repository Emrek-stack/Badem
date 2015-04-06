using System.Collections.Generic;
using Bade.Entity.Domain;

namespace Bade.Data.Contract
{
    public interface IAdminRepository
    {
        IList<Permission> GetAdminPermissionsById(int id);
    }
}