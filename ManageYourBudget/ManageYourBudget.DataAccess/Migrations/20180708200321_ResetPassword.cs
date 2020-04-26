using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageYourBudget.DataAccess.Migrations
{
    public partial class ResetPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordHash",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetPasswordHashExpirationTime",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetPasswordHash",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ResetPasswordHashExpirationTime",
                table: "AspNetUsers");
        }
    }
}
