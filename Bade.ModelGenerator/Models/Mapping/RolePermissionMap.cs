using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Bade.ModelGenerator.Models.Mapping
{
    public class RolePermissionMap : EntityTypeConfiguration<RolePermission>
    {
        public RolePermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("RolePermission");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.PermissionId).HasColumnName("PermissionId");

            // Relationships
            this.HasRequired(t => t.Permission)
                .WithMany(t => t.RolePermissions)
                .HasForeignKey(d => d.PermissionId);
            this.HasRequired(t => t.Role)
                .WithMany(t => t.RolePermissions)
                .HasForeignKey(d => d.RoleId);

        }
    }
}
