using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillEase360_CodeFirstApproach.Invoice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceTypeToInvoiceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PaymentStatus",
                table: "Invoices",
                type: "VARCHAR(20)",
                nullable: false,
                defaultValue: "Sales",
                oldClrType: typeof(string),
                oldType: "VARCHAR(20)",
                oldDefaultValue: "Pending");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceType",
                table: "Invoices",
                type: "VARCHAR(20)",
                nullable: false,
                defaultValue: "Pending",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PaymentStatus",
                table: "Invoices",
                type: "VARCHAR(20)",
                nullable: false,
                defaultValue: "Pending",
                oldClrType: typeof(string),
                oldType: "VARCHAR(20)",
                oldDefaultValue: "Sales");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceType",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(20)",
                oldDefaultValue: "Pending");
        }
    }
}
