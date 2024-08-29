using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDepositTransactionColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionTime",
                table: "Orders",
                newName: "TransactionTime2");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "Orders",
                newName: "TransactionTime1");

            migrationBuilder.RenameColumn(
                name: "IsPaid",
                table: "Orders",
                newName: "IsDonePaid");

            migrationBuilder.AddColumn<double>(
                name: "AmountTransaction1",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AmountTransaction2",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "TransactionId1",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionId2",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountTransaction1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AmountTransaction2",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TransactionId1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TransactionId2",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "TransactionTime2",
                table: "Orders",
                newName: "TransactionTime");

            migrationBuilder.RenameColumn(
                name: "TransactionTime1",
                table: "Orders",
                newName: "TransactionId");

            migrationBuilder.RenameColumn(
                name: "IsDonePaid",
                table: "Orders",
                newName: "IsPaid");
        }
    }
}
