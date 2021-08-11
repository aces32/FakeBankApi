using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeBank.BankAPI.Persitence.Migrations
{
    public partial class UpdateTransactionHistoryColumntype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "AccountBalanceBeforeDebit",
                table: "TransactionHistory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccountBalanceBeforeDebit",
                table: "TransactionHistory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
