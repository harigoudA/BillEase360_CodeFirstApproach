using BillEase360_CodeFirstApproach.Invoice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BillEase360_CodeFirstApproach.Invoice.Infrastructure
{
    public class InvoiceDbContext:DbContext
    {
        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options ):base(options) { }

        public DbSet<Invoices> Invoices { get; set; }

        public DbSet<InvoiceGSTDetails> InvoiceGSTDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Invoices>(entity => {

                entity.ToTable("Invoices");

                entity.HasKey(i=>i.InvoiceID);

                entity.Property(i => i.InvoiceID)
                .IsRequired()
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

                entity.Property(i=>i.UserID)
                .IsRequired();

                entity.Property(i=>i.CustomerID)
                .IsRequired();

                entity.Property(i => i.InvoiceNumber)
                .IsRequired()
                .HasColumnType("VARCHAR(60)");


                entity.HasIndex(i => i.InvoiceNumber)
                .IsUnique()
                .HasDatabaseName("UQ_Invoices_InvoiceNumber");

                entity.Property(i => i.InvoiceDate)
                .HasColumnType("DATETIME")
                .IsRequired();

                entity.Property(i => i.TotalAmount)
                .IsRequired()
                .HasColumnType("DECIMAL(12,2)");

                entity.Property(i => i.TaxableAmount)
                .IsRequired()
                .HasColumnType("DECIMAL(12,2)");

                entity.Property(i => i.PaymentStatus)
                .IsRequired()
                .HasColumnType("VARCHAR(20)")
                .HasDefaultValue("Sales");

                entity.Property(i => i.Status)
                .HasColumnType("VARCHAR(20)")
                .HasDefaultValue("Draft");

                entity.Property(i => i.InvoiceType)
                .HasColumnType("VARCHAR(20)")
                .HasDefaultValue("Pending");

                entity.Property(i=>i.IsActive)
                .HasColumnType("BIT")
                .HasDefaultValue(true);

                entity.Property(i => i.CreatedDate)
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()");

                entity.Property(i => i.ModifiedDate)
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()");

            });

            modelBuilder.Entity<InvoiceGSTDetails>(entity =>
            {
                entity.ToTable("InvoiceGSTDetails");

                entity.HasKey(ig => ig.GSTDetailID);

                entity.Property(ig => ig.GSTDetailID)
                .IsRequired()
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

                entity.Property(ig=>ig.InvoiceID)
                .IsRequired();

                entity.Property(ig => ig.GSTType)
                .HasColumnType("VARCHAR(20)")
                .IsRequired();

                entity.Property(ig=>ig.Rate)
                .HasColumnType("DECIMAL(4,2)")
                .IsRequired ();

                entity.Property(ig=>ig.TaxAmount)
                .HasColumnType("DECIMAL(12,2)")
                .IsRequired();

                entity.Property(ig => ig.IsActive)
                .HasColumnType("BIT")
                .HasDefaultValue(true);

                entity.Property(ig => ig.CreatedDate)
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()");

                entity.Property(ig => ig.ModifiedDate)
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()");

                entity.HasOne(ig=>ig.Invoices)
                .WithMany()
                .HasForeignKey(ig => ig.InvoiceID)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<InvoiceLineItems>(entity =>
            {
                entity.ToTable("InvoiceLineItems");

                entity.HasKey(il => il.LineItemID);

                entity.Property(il => il.LineItemID)
                .IsRequired()
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

                entity.Property(il => il.InvoiceID)
                .IsRequired();

                entity.Property(il => il.ProductID)
                .IsRequired();

                entity.Property(il => il.Quantity)
                .HasColumnType("DECIMAL(10,2)")
                .IsRequired();

                entity.Property(il => il.UnitPrice)
                .HasColumnType("DECIMAL(10,2)")
                .IsRequired();

                entity.Property(il => il.Discount)
                .HasColumnType("DECIMAL(10,2)")
                .IsRequired();

                entity.Property(il => il.TaxableValue)
                .HasColumnType("DECIMAL(12,2)")
                .IsRequired();

                entity.Property(il => il.GSTRate)
                .HasColumnType("DECIMAL(4,2)")
                .IsRequired();

                entity.Property(il => il.IsActive)
                .HasColumnType("BIT")
                .HasDefaultValue(true);

                entity.Property(il => il.CreatedDate)
               .HasColumnType("DATETIME")
               .HasDefaultValueSql("GETDATE()");

                entity.Property(il => il.ModifiedDate)
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()");


                });

            }

    }
}
