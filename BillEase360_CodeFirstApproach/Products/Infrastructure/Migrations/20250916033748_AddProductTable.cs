using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillEase360_CodeFirstApproach.Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxSlabID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HSN_SAC_Code = table.Column<string>(type: "VARCHAR(15)", maxLength: 15, nullable: false),
                    GST_Rate = table.Column<decimal>(type: "DECIMAL(4,2)", nullable: false, defaultValueSql: "0"),
                    Unit = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    SalesPrice = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    StockQty = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false, defaultValueSql: "0"),
                    ReorderLevel = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false, defaultValueSql: "0"),
                    IsComposition = table.Column<bool>(type: "BIT", nullable: false, defaultValueSql: "0"),
                    IsActive = table.Column<bool>(type: "BIT", nullable: false, defaultValueSql: "1"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_TaxSlabs_TaxSlabID",
                        column: x => x.TaxSlabID,
                        principalTable: "TaxSlabs",
                        principalColumn: "TaxSlabID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TaxSlabID",
                table: "Products",
                column: "TaxSlabID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
