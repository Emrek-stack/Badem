using System;
using System.Collections.Generic;

namespace Bade.ModelGenerator.Models
{
    public partial class Admin
    {
        public Admin()
        {
            this.Admin1 = new List<Admin>();
            this.AdminPermissions = new List<AdminPermission>();
            this.AdminRoles = new List<AdminRole>();
        }

        public int Id { get; set; }
        public Nullable<int> ParentAdminId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public int StatusId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public virtual ICollection<Admin> Admin1 { get; set; }
        public virtual Admin Admin2 { get; set; }
        public virtual ICollection<AdminPermission> AdminPermissions { get; set; }
        public virtual ICollection<AdminRole> AdminRoles { get; set; }
    }
}
