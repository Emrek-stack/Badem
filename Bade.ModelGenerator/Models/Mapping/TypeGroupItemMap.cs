using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Bade.ModelGenerator.Models.Mapping
{
    public class TypeGroupItemMap : EntityTypeConfiguration<TypeGroupItem>
    {
        public TypeGroupItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("TypeGroupItem", "Variable");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TypeGroupId).HasColumnName("TypeGroupId");
            this.Property(t => t.TypeId).HasColumnName("TypeId");

            // Relationships
            this.HasRequired(t => t.Type)
                .WithMany(t => t.TypeGroupItems)
                .HasForeignKey(d => d.TypeId);
            this.HasRequired(t => t.TypeGroup)
                .WithMany(t => t.TypeGroupItems)
                .HasForeignKey(d => d.TypeGroupId);

        }
    }
}
