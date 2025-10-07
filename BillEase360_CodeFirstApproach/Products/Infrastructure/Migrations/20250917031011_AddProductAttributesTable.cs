using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillEase360_CodeFirstApproach.Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductAttributesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    AttributeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeName = table.Column<string>(type: "VARCHAR(60)", nullable: false),
                    AttributeValue = table.Column<string>(type: "VARCHAR(MAX)", nullable: false),
                    AttributeType = table.Column<string>(type: "VARCHAR(10)", nullable: false),
                    IsActive = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.AttributeID);
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_ProductAttribute",
                table: "ProductAttributes",
                columns: new[] { "ProductID", "AttributeName" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAttributes");
        }
    }
}
