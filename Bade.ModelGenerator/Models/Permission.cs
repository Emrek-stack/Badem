using System;
using System.Collections.Generic;

namespace Bade.ModelGenerator.Models
{
    public partial class Permission
    {
        public Permission()
        {
            this.AdminPermissions = new List<AdminPermission>();
            this.RolePermissions = new List<RolePermission>();
        }

        public int Id { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public virtual ICollection<AdminPermission> AdminPermissions { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
