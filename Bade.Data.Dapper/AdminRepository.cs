using System.Collections.Generic;
using System.Data;
using System.Linq;
using Bade.Constants;
using Bade.Data.Contract;
using Bade.Entity.Domain;
using Bade.Infrastructure;
using Dapper;

namespace Bade.Data.Dapper
{
    public class AdminRepository :Repository, IAdminRepository
    {

        public AdminRepository(IConnectionFactory connectionFactory)
            : base(connectionFactory, Constant.DefaultConnectionStringName)
        {

        }
        public IList<Permission> GetAdminPermissionsById(int id)
        {
            return GetManyStoredProcedure<Permission>("GetAdminPermissionsById", new {AdminId = id}).ToList();

          //var data =  GetConnection(
          //      c =>
          //          c.Query<Permission>("GetAdminPermissionsById", new {AdminId = id},
          //              commandType: CommandType.StoredProcedure)).ToList();
          //  return data;
        }
    }
}