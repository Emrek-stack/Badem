using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Bade.ModelGenerator.Models.Mapping
{
    public class MemberDetailMap : EntityTypeConfiguration<MemberDetail>
    {
        public MemberDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.About)
                .HasMaxLength(500);

            this.Property(t => t.Motto)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("MemberDetail", "Membership");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MemberId).HasColumnName("MemberId");
            this.Property(t => t.About).HasColumnName("About");
            this.Property(t => t.Motto).HasColumnName("Motto");
            this.Property(t => t.RegistrationSource).HasColumnName("RegistrationSource");
            this.Property(t => t.LastLogin).HasColumnName("LastLogin");

            // Relationships
            this.HasRequired(t => t.Member)
                .WithMany(t => t.MemberDetails)
                .HasForeignKey(d => d.MemberId);

        }
    }
}
