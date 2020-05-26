using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageYourBudget.DataAccess.Migrations
{
    public partial class ExpensesAdditionalFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_CyclicExpenses_CyclicExpenseId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "CyclicExpenses");

            migrationBuilder.AlterColumn<int>(
                name: "CyclicExpenseId",
                table: "Expenses",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Expenses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "CyclicExpenses",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastApplied",
                table: "CyclicExpenses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CreatedById",
                table: "Expenses",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CyclicExpenses_CreatedById",
                table: "CyclicExpenses",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_CyclicExpenses_AspNetUsers_CreatedById",
                table: "CyclicExpenses",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_AspNetUsers_CreatedById",
                table: "Expenses",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_CyclicExpenses_CyclicExpenseId",
                table: "Expenses",
                column: "CyclicExpenseId",
                principalTable: "CyclicExpenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CyclicExpenses_AspNetUsers_CreatedById",
                table: "CyclicExpenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_AspNetUsers_CreatedById",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_CyclicExpenses_CyclicExpenseId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_CreatedById",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_CyclicExpenses_CreatedById",
                table: "CyclicExpenses");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "CyclicExpenses");

            migrationBuilder.DropColumn(
                name: "LastApplied",
                table: "CyclicExpenses");

            migrationBuilder.AlterColumn<int>(
                name: "CyclicExpenseId",
                table: "Expenses",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Expenses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "CyclicExpenses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_CyclicExpenses_CyclicExpenseId",
                table: "Expenses",
                column: "CyclicExpenseId",
                principalTable: "CyclicExpenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
