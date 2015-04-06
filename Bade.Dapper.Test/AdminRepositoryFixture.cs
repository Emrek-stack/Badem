using Bade.Data.Contract;
using Bade.Infrastructure;
using NUnit.Framework;

namespace Bade.Data.Dapper.Test
{
    [TestFixture]
    public class AdminRepositoryFixture
    {
        [Test]
        public void get_admin_permission()
        {
            IAdminRepository adminRepository = new AdminRepository(new MsSqlConnectionFactory());
            var data = adminRepository.GetAdminPermissionsById(1);
        }
    }
}