using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillEase360_CodeFirstApproach.BusinessUsers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBankDetailsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessUserBankDetails",
                columns: table => new
                {
                    BankDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    BusinessUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    AccountNumber = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    IFSCCode = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    BranchName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    IsActive = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessUserBankDetails", x => x.BankDetailID);
                    table.ForeignKey(
                        name: "FK_BusinessUserBankDetails_BusinessUsers_BusinessUserID",
                        column: x => x.BusinessUserID,
                        principalTable: "BusinessUsers",
                        principalColumn: "BusinessUserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessUserBankDetails_BusinessUserID",
                table: "BusinessUserBankDetails",
                column: "BusinessUserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessUserBankDetails");
        }
    }
}
