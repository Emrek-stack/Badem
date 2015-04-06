using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Bade.ModelGenerator.Models.Mapping;

namespace Bade.ModelGenerator.Models
{
    public partial class BadeDBContext : DbContext
    {
        static BadeDBContext()
        {
            Database.SetInitializer<BadeDBContext>(null);
        }

        public BadeDBContext()
            : base("Name=BadeDBContext")
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<AdminPermission> AdminPermissions { get; set; }
        public DbSet<AdminRole> AdminRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberDetail> MemberDetails { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationConfig> ApplicationConfigs { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<StatusGroup> StatusGroups { get; set; }
        public DbSet<StatusGroupItem> StatusGroupItems { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<TypeGroup> TypeGroups { get; set; }
        public DbSet<TypeGroupItem> TypeGroupItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AdminMap());
            modelBuilder.Configurations.Add(new AdminPermissionMap());
            modelBuilder.Configurations.Add(new AdminRoleMap());
            modelBuilder.Configurations.Add(new PermissionMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new RolePermissionMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new MemberMap());
            modelBuilder.Configurations.Add(new MemberDetailMap());
            modelBuilder.Configurations.Add(new ApplicationMap());
            modelBuilder.Configurations.Add(new ApplicationConfigMap());
            modelBuilder.Configurations.Add(new StatusMap());
            modelBuilder.Configurations.Add(new StatusGroupMap());
            modelBuilder.Configurations.Add(new StatusGroupItemMap());
            modelBuilder.Configurations.Add(new TypeMap());
            modelBuilder.Configurations.Add(new TypeGroupMap());
            modelBuilder.Configurations.Add(new TypeGroupItemMap());
        }
    }
}
