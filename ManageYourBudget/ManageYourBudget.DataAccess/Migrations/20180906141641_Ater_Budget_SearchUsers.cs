using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageYourBudget.DataAccess.Migrations
{
    public partial class Ater_Budget_SearchUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var budgetSearchUsers =
                @"ALTER PROCEDURE budget_SearchUsers @searchTerm varchar(max), @currentUserId varchar(max), @batchSize int
                AS
                BEGIN
	                SELECT TOP(@batchSize) *
	                FROM AspNetUsers u
	                WHERE (@searchTerm IS NULL OR (LOWER(CONCAT(u.FirstName, ' ', u.LastName)) LIKE @searchTerm 
		                  OR LOWER(u.Email) LIKE @searchTerm))
		                  AND u.Id != @currentUserId
	                ORDER BY u.Created DESC
                END";
            migrationBuilder.Sql(budgetSearchUsers);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
