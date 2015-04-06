using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Bade.ModelGenerator.Models.Mapping
{
    public class MemberMap : EntityTypeConfiguration<Member>
    {
        public MemberMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.UniqueName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Firstname)
                .HasMaxLength(50);

            this.Property(t => t.Lastname)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Member", "Membership");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UniqueName).HasColumnName("UniqueName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.Firstname).HasColumnName("Firstname");
            this.Property(t => t.Lastname).HasColumnName("Lastname");
            this.Property(t => t.BirthDate).HasColumnName("BirthDate");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
