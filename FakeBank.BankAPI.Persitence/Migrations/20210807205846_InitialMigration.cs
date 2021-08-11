using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeBank.BankAPI.Persitence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomersID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomersID);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LedgerBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClearedBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomersID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountsID);
                    table.ForeignKey(
                        name: "FK_Accounts_Customers_CustomersID",
                        column: x => x.CustomersID,
                        principalTable: "Customers",
                        principalColumn: "CustomersID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionHistory",
                columns: table => new
                {
                    TransactionHistoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentReference = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PaymentDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TransactionAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionRemarks = table.Column<decimal>(type: "decimal(18,2)", maxLength: 150, nullable: false),
                    DebCredFlag = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    BeneficiaryAccountNumber = table.Column<int>(type: "int", nullable: false),
                    BeneficiaryAccountName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AccountsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistory", x => x.TransactionHistoryID);
                    table.ForeignKey(
                        name: "FK_TransactionHistory_Accounts_AccountsID",
                        column: x => x.AccountsID,
                        principalTable: "Accounts",
                        principalColumn: "AccountsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomersID", "CreatedBy", "CreatedDate", "CustomerName", "LastModifiedBy", "LastModifiedDate" },
                values: new object[,]
                {
                    { 1, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Arisha Barron", null, null },
                    { 2, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Branden Gibson", null, null },
                    { 3, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Rhonda Church", null, null },
                    { 4, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Georgina Hazel", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomersID",
                table: "Accounts",
                column: "CustomersID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_AccountsID",
                table: "TransactionHistory",
                column: "AccountsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionHistory");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
