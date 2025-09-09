using BillEase360_CodeFirstApproach.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillEase360_CodeFirstApproach.Users.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Users config
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.Property(u => u.UserId)
                      .HasDefaultValueSql("NEWID()");

                entity.Property(u => u.CreatedDate)
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(u => u.ModifiedDate)
                      .HasDefaultValueSql("GETDATE()");
            });

            // Roles config
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles");

                entity.HasKey(r => r.RoleId);

                entity.Property(r => r.RoleId)
                      .HasDefaultValueSql("NEWSEQUENTIALID()");

                entity.Property(r => r.CreatedDate)
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(r => r.ModifiedDate)
                      .HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRoles");

                entity.HasKey(ur => ur.UserRoleId);

                entity.Property(ur => ur.UserRoleId)
                      .HasDefaultValueSql("NEWID()");

                entity.Property(ur => ur.CreatedDate)
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(ur => ur.ModifiedDate)
                      .HasDefaultValueSql("GETDATE()");

                // Relationships
                entity.HasOne(ur => ur.User)
                      .WithMany(u => u.UserRoles)   // 👈 collection in User
                      .HasForeignKey(ur => ur.UserId);

                entity.HasOne(ur => ur.Role)
                      .WithMany(r => r.UserRoles)   // 👈 collection in Role
                      .HasForeignKey(ur => ur.RoleId);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permissions");

                entity.HasKey(p => p.PermissionId);

                entity.Property(p => p.PermissionId)
                      .HasDefaultValueSql("NEWID()");

                entity.Property(p => p.CreatedDate)
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(p => p.ModifiedDate)
                      .HasDefaultValueSql("GETDATE()");
            });


            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.ToTable("RolePermissions");

                entity.HasKey(rp => rp.RolePermissionId);

                entity.Property(rp => rp.RolePermissionId)
                      .HasDefaultValueSql("NEWID()");

                entity.Property(rp => rp.CreatedDate)
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(rp => rp.ModifiedDate)
                      .HasDefaultValueSql("GETDATE()");

                // Relationships
                entity.HasOne(rp => rp.Role)
                      .WithMany(r => r.RolePermissions)   // 👈 Add collection in Role
                      .HasForeignKey(rp => rp.RoleId);

                entity.HasOne(rp => rp.Permission)
                      .WithMany(p => p.RolePermissions)   // 👈 Add collection in Permission
                      .HasForeignKey(rp => rp.PermissionId);
            });


        }
    }
}
