using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageYourBudget.DataAccess.Migrations
{
    public partial class ExtendWalletModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "Wallets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "UserWallets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastOpened",
                table: "UserWallets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archived",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "Archived",
                table: "UserWallets");

            migrationBuilder.DropColumn(
                name: "LastOpened",
                table: "UserWallets");
        }
    }
}
