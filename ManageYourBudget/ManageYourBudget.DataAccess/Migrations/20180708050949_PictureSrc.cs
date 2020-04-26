using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageYourBudget.DataAccess.Migrations
{
    public partial class PictureSrc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureSrc",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureSrc",
                table: "AspNetUsers");
        }
    }
}
