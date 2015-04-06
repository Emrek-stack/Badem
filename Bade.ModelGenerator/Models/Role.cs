using System;
using System.Collections.Generic;

namespace Bade.ModelGenerator.Models
{
    public partial class Role
    {
        public Role()
        {
            this.AdminRoles = new List<AdminRole>();
            this.RolePermissions = new List<RolePermission>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<AdminRole> AdminRoles { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
