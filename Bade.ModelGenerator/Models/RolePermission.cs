using System;
using System.Collections.Generic;

namespace Bade.ModelGenerator.Models
{
    public partial class RolePermission
    {
        public int Id { get; set; }
        public short RoleId { get; set; }
        public int PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
        public virtual Role Role { get; set; }
    }
}
