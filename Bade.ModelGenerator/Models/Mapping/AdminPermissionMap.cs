using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Bade.ModelGenerator.Models.Mapping
{
    public class AdminPermissionMap : EntityTypeConfiguration<AdminPermission>
    {
        public AdminPermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("AdminPermission");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AdminId).HasColumnName("AdminId");
            this.Property(t => t.PermissionId).HasColumnName("PermissionId");
            this.Property(t => t.IsAccessible).HasColumnName("IsAccessible");

            // Relationships
            this.HasRequired(t => t.Admin)
                .WithMany(t => t.AdminPermissions)
                .HasForeignKey(d => d.AdminId);
            this.HasRequired(t => t.Permission)
                .WithMany(t => t.AdminPermissions)
                .HasForeignKey(d => d.PermissionId);

        }
    }
}
