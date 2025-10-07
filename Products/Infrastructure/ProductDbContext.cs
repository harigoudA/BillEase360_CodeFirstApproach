using BillEase360_CodeFirstApproach.Products.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace BillEase360_CodeFirstApproach.Products.Infrastructure
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }
        public DbSet<Categories> Categories { get; set; }

        public DbSet<TaxSlabs> TaxSlabs { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<ProductAttributes> ProductAttributes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Categories
            modelBuilder.Entity<Categories>(entity => {
                entity.ToTable("Categories");
                entity.Property(e => e.CategoryId).HasDefaultValueSql("NEWID()");
                entity.Property(e => e.CategoryName).IsRequired().HasColumnType("Varchar(100)");
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.ModifiedDate).HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<TaxSlabs>(entity =>
            {
                entity.ToTable("TaxSlabs");

                entity.HasKey(t => t.TaxSlabID);
                entity.Property(t => t.TaxSlabID)
                      .HasDefaultValueSql("NEWID()")
                      .ValueGeneratedOnAdd();

                entity.Property(t => t.SlabName)
                      .HasColumnType("VARCHAR(60)")
                      .IsRequired();

                entity.Property(t => t.GST)
                      .HasColumnType("DECIMAL(5,2)")
                      .HasDefaultValue(0);

                entity.Property(t => t.CreatedDate)
                      .HasDefaultValueSql("GETDATE()");
                entity.Property(t => t.ModifiedDate)
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(t => t.EffectiveFrom)
                      .HasColumnType("DATE");
                entity.Property(t => t.EffectiveTo)
                      .HasColumnType("DATE");

                entity.Property(t => t.IsActive)
                      .HasDefaultValue(true);
            });

            modelBuilder.Entity<Product>(entity => {
                entity.ToTable("Products");

                entity.Property(p => p.ProductID)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

                entity.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

                entity.Property(p => p.HSN_SAC_Code)
                .HasColumnType("VARCHAR(15)");

                entity.Property(p => p.GST_Rate)
                .IsRequired()
                .HasColumnType("DECIMAL(4,2)")
                .HasDefaultValueSql("0");

                entity.Property(p => p.Unit)
                .HasColumnType("VARCHAR(20)");

                entity.Property(p => p.SalesPrice)
                .HasColumnType("DECIMAL(10,2)");

                entity.Property(p => p.PurchasePrice)
                .HasColumnType("DECIMAL(10,2)");

                entity.Property(p => p.Description)
                .HasColumnType("VARCHAR(250)");

                entity.Property(p=>p.StockQty)
                .HasColumnType("DECIMAL(10,2)")
                .HasDefaultValueSql("0");

                entity.Property(p=>p.ReorderLevel)
                .HasColumnType("DECIMAL(10,2)")
                .HasDefaultValueSql("0");

                entity.Property(p=>p.IsComposition)
                .HasColumnType("BIT")
                .HasDefaultValueSql ("0");

                entity.Property(p => p.IsActive)
                .HasColumnType("BIT")
                .HasDefaultValueSql("1");

                entity.Property(p => p.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

                entity.Property(p => p.ModifiedDate)
                .HasDefaultValueSql("GETDATE()");

                entity.HasOne(p=>p.Category)
                .WithMany()
                .HasForeignKey(p=>p.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p=>p.TaxSlab)
                .WithMany()
                .HasForeignKey(p=>p.TaxSlabID)
                .OnDelete(DeleteBehavior.Restrict);


            });

            modelBuilder.Entity<ProductAttributes>(entity => {

                entity.ToTable("ProductAttributes");

                entity.HasKey(pa => pa.AttributeID);

                entity.Property(pa => pa.AttributeID)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

                entity.Property(pa => pa.AttributeName)
                .IsRequired()
                .HasColumnType("VARCHAR(60)");

                entity.Property(pa => pa.AttributeValue)
                .HasColumnType("VARCHAR(MAX)");

                entity.Property(pa => pa.AttributeType)
                .HasColumnType("VARCHAR(10)");

                entity.Property(pa => pa.IsActive)
                .HasColumnType("BIT")
                .HasDefaultValue(true);

                entity.Property(pa => pa.CreatedDate)
                .HasColumnType("DATETIME")
               .HasDefaultValueSql("GETDATE()");

                entity.Property(pa => pa.ModifiedDate)
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()");

                entity.HasOne(pa=>pa.Product)
                .WithMany()
                .HasForeignKey(pa=>pa.ProductID)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(pa => new {pa.ProductID,pa.AttributeName})
                .IsUnique()
                .HasDatabaseName("UQ_ProductAttribute");

            });
        }
    }
}



