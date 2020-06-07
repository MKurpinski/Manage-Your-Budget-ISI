using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageYourBudget.DataAccess.Migrations
{
    public partial class Budget_SearchExpenses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var searchExpenses =
                @"CREATE PROCEDURE budget_SearchExpenses
	                @searchTerm varchar(max),
	                @walletId int,
	                @dateFrom datetime2,
                    @dateTo datetime2,
	                @batchSize int,
	                @toSkip int,
	                @currentUserId varchar(30),
	                @category int NULL,
	                @type int NULL
                AS
                BEGIN
	                DECLARE @sqlSearchTerm varchar(max)
	                SET @sqlSearchTerm = CONCAT('%', @searchTerm, '%')
	                SELECT 
	                e.Id,
	                e.Name,
	                e.Place,
	                e.Price,
	                e.Category,
					e.Date,
	                e.Type,
	                e.Created,
	                u.FirstName as CreatedByName,
	                u.LastName as CreatedByLastName,
	                u.Email as CreatedByEmail,
                    u.PictureSrc as CreatedByPictureSrc,
	                COUNT(*) OVER() as Total
	                FROM Expenses e 
	                INNER JOIN AspNetUsers u ON u.Id = e.CreatedById
	                WHERE 
					e.WalletId = @walletId 
					AND
	                (@searchTerm = '' OR (@searchTerm <> '' AND (e.Place LIKE @sqlSearchTerm OR e.Name LIKE @sqlSearchTerm)))
	                AND 
	                (@category IS NULL OR e.Category = @category)
	                AND
	                (@type IS NULL OR e.Type = @type)
	                AND
	                (e.Date BETWEEN @dateFrom AND @dateTo)
	                ORDER BY e.Created DESC
	                OFFSET @toSkip ROWS
	                FETCH NEXT @batchSize ROWS ONLY;
                END";
            migrationBuilder.Sql(searchExpenses);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
