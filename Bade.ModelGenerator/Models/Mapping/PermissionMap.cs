using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Bade.ModelGenerator.Models.Mapping
{
    public class PermissionMap : EntityTypeConfiguration<Permission>
    {
        public PermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Key)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Permission");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
