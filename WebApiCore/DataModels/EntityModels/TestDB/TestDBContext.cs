using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApiCore.DataModels.EntityModels.TestDB
{
    public partial class TestDBContext : DbContext
    {
        public TestDBContext()
        {
        }

        public TestDBContext(DbContextOptions<TestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CrmContact> CrmContact { get; set; }
        public virtual DbSet<SecurityMenu> SecurityMenu { get; set; }
        public virtual DbSet<SecurityModule> SecurityModule { get; set; }
        public virtual DbSet<SecurityPermission> SecurityPermission { get; set; }
        public virtual DbSet<SecurityRole> SecurityRole { get; set; }
        public virtual DbSet<SecurityUser> SecurityUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-UEI4BLI;Database=TestDB;User Id=sa; Password=123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CrmContact>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.Property(e => e.ContactName).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SecurityMenu>(entity =>
            {
                entity.HasKey(e => e.MenuId);

                entity.Property(e => e.Controller)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MenuName).HasMaxLength(50);
            });

            modelBuilder.Entity<SecurityModule>(entity =>
            {
                entity.HasKey(e => e.ModuleId);

                entity.Property(e => e.ModuleName).HasMaxLength(50);
            });

            modelBuilder.Entity<SecurityPermission>(entity =>
            {
                entity.HasKey(e => e.PermissionId);

                entity.Property(e => e.Controller)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MenuName).HasMaxLength(50);
            });

            modelBuilder.Entity<SecurityRole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<SecurityUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserPass)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
