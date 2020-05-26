using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageYourBudget.DataAccess.Migrations
{
    public partial class Expenses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CyclicExpenses",
                columns: table => new
                {
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Place = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Category = table.Column<int>(nullable: false),
                    Currency = table.Column<int>(nullable: false),
                    WalletId = table.Column<int>(nullable: false),
                    ModifiedById = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    PeriodType = table.Column<int>(nullable: false),
                    StartingFrom = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CyclicExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CyclicExpenses_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CyclicExpenses_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Place = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Category = table.Column<int>(nullable: false),
                    Currency = table.Column<int>(nullable: false),
                    WalletId = table.Column<int>(nullable: false),
                    ModifiedById = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    CyclicExpenseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_CyclicExpenses_CyclicExpenseId",
                        column: x => x.CyclicExpenseId,
                        principalTable: "CyclicExpenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Expenses_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CyclicExpenses_ModifiedById",
                table: "CyclicExpenses",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CyclicExpenses_WalletId",
                table: "CyclicExpenses",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CyclicExpenseId",
                table: "Expenses",
                column: "CyclicExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ModifiedById",
                table: "Expenses",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_WalletId",
                table: "Expenses",
                column: "WalletId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "CyclicExpenses");
        }
    }
}
