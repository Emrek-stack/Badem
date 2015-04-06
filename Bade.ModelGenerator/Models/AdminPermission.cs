using System;
using System.Collections.Generic;

namespace Bade.ModelGenerator.Models
{
    public partial class AdminPermission
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public int PermissionId { get; set; }
        public bool IsAccessible { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
