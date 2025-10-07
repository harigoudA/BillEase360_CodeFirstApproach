using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillEase360_CodeFirstApproach.BusinessUsers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBusinessUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessUsers",
                columns: table => new
                {
                    BusinessUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    BusinessUserType = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    ContactNumber = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    City = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    State = table.Column<string>(type: "VARCHAR(10)", nullable: false, defaultValue: "Active"),
                    PostalCode = table.Column<string>(type: "VARCHAR(15)", nullable: false),
                    GSTIN = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessUsers", x => x.BusinessUserID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessUsers");
        }
    }
}
