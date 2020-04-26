using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageYourBudget.DataAccess.Migrations
{
    public partial class Budget_SearchUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var budgetSearchUsers =
                @"CREATE PROCEDURE budget_SearchUsers @searchTerm varchar(max), @currentUserId varchar(max), @batchSize int, @toSkip int
                AS
                BEGIN
	                SELECT *
	                FROM AspNetUsers u
	                WHERE (@searchTerm IS NULL OR (LOWER(CONCAT(u.FirstName, ' ', u.LastName)) LIKE @searchTerm 
		                  OR LOWER(u.Email) LIKE @searchTerm))
		                  AND u.Id != @currentUserId
	                ORDER BY u.Created DESC
	                OFFSET @toSkip ROWS
	                FETCH NEXT @batchSize ROWS ONLY
                END";
            migrationBuilder.Sql(budgetSearchUsers);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var deleteBudgetSearchUsers = @"DROP PROCEDURE budget_SearchUsers";
            migrationBuilder.Sql(deleteBudgetSearchUsers);
        }
    }
}
