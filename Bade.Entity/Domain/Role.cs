using System.Collections.Generic;

namespace Bade.Entity.Domain
{
    public class Role
    {
        public short Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public bool IsActive { get; set; }
        
        public virtual ICollection<AdminRole> AdminRoles { get; set; }
        
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
