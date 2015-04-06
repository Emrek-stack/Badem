using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Bade.ModelGenerator.Models.Mapping
{
    public class AdminMap : EntityTypeConfiguration<Admin>
    {
        public AdminMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Surname)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.PasswordHash)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.PasswordSalt)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Admin");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ParentAdminId).HasColumnName("ParentAdminId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Surname).HasColumnName("Surname");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.PasswordHash).HasColumnName("PasswordHash");
            this.Property(t => t.PasswordSalt).HasColumnName("PasswordSalt");
            this.Property(t => t.StatusId).HasColumnName("StatusId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");

            // Relationships
            this.HasOptional(t => t.Admin2)
                .WithMany(t => t.Admin1)
                .HasForeignKey(d => d.ParentAdminId);

        }
    }
}
