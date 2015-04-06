using System;
using System.Collections.Generic;
using Bade.Infrastructure;

namespace Bade.Entity.Domain
{
    public class Admin : IAggregateRoot
    {
        public int Id { get; set; }
        
        public int? ParentAdminId { get; set; }
        
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string Email { get; set; }
        
        public string PasswordHash { get; set; }
        
        public string PasswordSalt { get; set; }
        
        public int StatusId { get; set; }
        
        public DateTime CreateDate { get; set; }
        
        public virtual ICollection<Admin> Admin1 { get; set; }
        
        public virtual Admin Admin2 { get; set; }
        
        public virtual ICollection<AdminPermission> AdminPermissions { get; set; }
        
        public virtual ICollection<AdminRole> AdminRoles { get; set; }
    }
}
