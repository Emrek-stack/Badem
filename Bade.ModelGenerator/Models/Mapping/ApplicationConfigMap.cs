using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Bade.ModelGenerator.Models.Mapping
{
    public class ApplicationConfigMap : EntityTypeConfiguration<ApplicationConfig>
    {
        public ApplicationConfigMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Key)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("ApplicationConfig", "Variable");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ApplicationId).HasColumnName("ApplicationId");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.Value).HasColumnName("Value");

            // Relationships
            this.HasRequired(t => t.Application)
                .WithMany(t => t.ApplicationConfigs)
                .HasForeignKey(d => d.ApplicationId);

        }
    }
}
