using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Bade.ModelGenerator.Models.Mapping
{
    public class AdminRoleMap : EntityTypeConfiguration<AdminRole>
    {
        public AdminRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("AdminRole");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AdminId).HasColumnName("AdminId");
            this.Property(t => t.RoleId).HasColumnName("RoleId");

            // Relationships
            this.HasRequired(t => t.Admin)
                .WithMany(t => t.AdminRoles)
                .HasForeignKey(d => d.AdminId);
            this.HasRequired(t => t.Role)
                .WithMany(t => t.AdminRoles)
                .HasForeignKey(d => d.RoleId);

        }
    }
}
