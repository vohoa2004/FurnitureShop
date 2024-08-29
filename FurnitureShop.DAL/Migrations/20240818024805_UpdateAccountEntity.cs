using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccountEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Accounts");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "BirthDay",
                table: "Accounts",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "BirthDay",
                table: "Accounts",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
