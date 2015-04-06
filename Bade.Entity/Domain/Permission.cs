using System.Collections.Generic;

namespace Bade.Entity.Domain
{
    public class Permission
    {
        public int Id { get; set; }
        
        public string Key { get; set; }
        
        public string Description { get; set; }
        
        public virtual ICollection<AdminPermission> AdminPermissions { get; set; }
        
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
