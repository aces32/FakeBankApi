using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeBank.BankAPI.Persitence.Migrations
{
    public partial class UpdateTransactionHistoryColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TransactionRemarks",
                table: "TransactionHistory",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "BeneficiaryAccountNumber",
                table: "TransactionHistory",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AccountBalanceBeforeDebit",
                table: "TransactionHistory",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountBalanceBeforeDebit",
                table: "TransactionHistory");

            migrationBuilder.AlterColumn<decimal>(
                name: "TransactionRemarks",
                table: "TransactionHistory",
                type: "decimal(18,2)",
                maxLength: 150,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BeneficiaryAccountNumber",
                table: "TransactionHistory",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
