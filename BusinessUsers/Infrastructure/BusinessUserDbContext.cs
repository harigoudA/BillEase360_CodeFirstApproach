using BillEase360_CodeFirstApproach.BusinessUsers.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillEase360_CodeFirstApproach.BusinessUsers.Infrastructure
{
    public class BusinessUserDbContext:DbContext
    {
        public BusinessUserDbContext(DbContextOptions<BusinessUserDbContext> options): base (options) { }

        public DbSet<BusinessUser> BusinessUsers { get; set; }

        public DbSet<BusinessUserBankDetails> businessUserBankDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BusinessUser>(entity =>
            {
                entity.ToTable("BusinessUsers");

                entity.HasKey(bu=>bu.BusinessUserID);

                entity.Property(bu => bu.BusinessUserID)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

                entity.Property(bu => bu.Name)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

                entity.Property(bu => bu.BusinessUserType)
                .IsRequired()
                .HasColumnType("VARCHAR(20)");

                entity.Property(bu => bu.ContactNumber)
                .HasColumnType("VARCHAR(20)");

                entity.Property(bu => bu.Email)
                .HasColumnType("VARCHAR(100)");

                entity.Property(bu => bu.AddressLine1)
                .HasColumnType("VARCHAR(250)");

                entity.Property(bu => bu.AddressLine2)
                .HasColumnType("VARCHAR(250)");

                entity.Property(bu => bu.City)
                .HasColumnType("VARCHAR(50)");

                entity.Property(bu => bu.State)
                .HasColumnType("VARCHAR(50)");

                entity.Property(bu => bu.PostalCode)
                .HasColumnType("VARCHAR(15)");

                entity.Property(bu => bu.GSTIN)
                .HasColumnType("VARCHAR(20)");

                entity.Property(bu => bu.State)
                .HasColumnType("VARCHAR(10)")
                .HasDefaultValue("Active");

                entity.Property(bu=>bu.IsActive)
                .HasColumnType("BIT")
                .HasDefaultValue(true);

                entity.Property(bu => bu.CreatedDate)
               .HasColumnType("DATETIME")
              .HasDefaultValueSql("GETDATE()");

                entity.Property(bu => bu.ModifiedDate)
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<BusinessUserBankDetails>(entity =>
            {

                entity.ToTable("BusinessUserBankDetails");

                entity.HasKey(bd=>bd.BankDetailID);

                entity.Property(bd => bd.BankDetailID)
                .HasDefaultValueSql("NEWID()")
                .IsRequired()
                .ValueGeneratedOnAdd();

                entity.Property(bd => bd.BusinessUserID)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

                entity.Property(bd => bd.BankName)
                .HasColumnType("VARCHAR(100)");

                entity.Property(bd => bd.AccountNumber)
                .HasColumnType("VARCHAR(30)");

                entity.Property(bd => bd.IFSCCode)
                .HasColumnType("VARCHAR(20)");

                entity.Property(bd => bd.BranchName)
                .HasColumnType("VARCHAR(100)");

                entity.Property(bd => bd.IsActive)
                .HasColumnType("BIT")
                .HasDefaultValue(true);

                entity.Property(bd => bd.CreatedDate)
             .HasColumnType("DATETIME")
            .HasDefaultValueSql("GETDATE()");

                entity.Property(bd => bd.ModifiedDate)
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()");

                entity.HasOne(bd=>bd.BusinessUsers)
                .WithMany()
                .HasForeignKey(bd=>bd.BusinessUserID)
                .OnDelete(DeleteBehavior.Restrict);

            });
        }
    }
}
