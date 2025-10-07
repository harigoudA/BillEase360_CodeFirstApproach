using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillEase360_CodeFirstApproach.Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTaxSlabTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
     name: "TaxSlabs",
     columns: table => new
     {
         TaxSlabID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
         SlabName = table.Column<string>(type: "VARCHAR(60)", nullable: false),
         GST = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false, defaultValue: 0m),
         EffectiveFrom = table.Column<DateOnly>(type: "DATE", nullable: false),
         EffectiveTo = table.Column<DateOnly>(type: "DATE", nullable: false),
         IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
         CreatedBy = table.Column<Guid>(nullable: true),
         ModifiedBy = table.Column<Guid>(nullable: true),
         CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
         ModifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
     },
     constraints: table =>
     {
         table.PrimaryKey("PK_TaxSlabs", x => x.TaxSlabID);
     });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxSlabs");
        }
    }
}
