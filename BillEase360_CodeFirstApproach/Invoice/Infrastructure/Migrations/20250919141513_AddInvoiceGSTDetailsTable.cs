using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillEase360_CodeFirstApproach.Invoice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceGSTDetailsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceGSTDetails",
                columns: table => new
                {
                    GSTDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    InvoiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GSTType = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    Rate = table.Column<decimal>(type: "DECIMAL(4,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "DECIMAL(12,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceGSTDetails", x => x.GSTDetailID);
                    table.ForeignKey(
                        name: "FK_InvoiceGSTDetails_Invoices_InvoiceID",
                        column: x => x.InvoiceID,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceGSTDetails_InvoiceID",
                table: "InvoiceGSTDetails",
                column: "InvoiceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceGSTDetails");
        }
    }
}
