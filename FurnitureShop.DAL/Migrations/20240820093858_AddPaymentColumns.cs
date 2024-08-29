using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionTime",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TransactionTime",
                table: "Orders");
        }
    }
}
