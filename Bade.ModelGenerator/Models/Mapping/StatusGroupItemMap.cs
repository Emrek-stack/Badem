using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Bade.ModelGenerator.Models.Mapping
{
    public class StatusGroupItemMap : EntityTypeConfiguration<StatusGroupItem>
    {
        public StatusGroupItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("StatusGroupItem", "Variable");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.StatusGroupId).HasColumnName("StatusGroupId");
            this.Property(t => t.StatusId).HasColumnName("StatusId");

            // Relationships
            this.HasRequired(t => t.Status)
                .WithMany(t => t.StatusGroupItems)
                .HasForeignKey(d => d.StatusId);
            this.HasRequired(t => t.StatusGroup)
                .WithMany(t => t.StatusGroupItems)
                .HasForeignKey(d => d.StatusGroupId);

        }
    }
}
