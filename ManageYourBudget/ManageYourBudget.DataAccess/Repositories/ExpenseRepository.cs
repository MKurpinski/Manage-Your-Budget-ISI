using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.Common.Extensions;
using ManageYourBudget.DataAccess.Interfaces;
using ManageYourBudget.DataAccess.Models.Expense;
using ManageYourBudget.Dtos.Expense;
using ManageYourBudget.Dtos.Search;
using Microsoft.EntityFrameworkCore;

namespace ManageYourBudget.DataAccess.Repositories
{
    public class ExpenseRepository: BaseRepository, IExpenseRepository
    {
        public ExpenseRepository(BudgetContext context) : base(context)
        {
        }

        public async Task CreateExpense(Expense expense)
        {
            await Context.AddEntity(expense);
            Context.SaveChanges();
        }

        public async Task<Expense> GetExpense(int id)
        {
            return await Context.Expenses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void DeleteExpense(Expense expense)
        {
            Context.Expenses.Remove(expense);
            Context.SaveChanges();
        }

        public int UpdateExpense(Expense expense)
        {
            Context.UpdateEntity(expense);
            return Context.SaveChanges();
        }

        public async Task<List<ExpenseSearchResult>> Search(ExpenseSearchOptionsDto searchOptions, string userId)
        {
            searchOptions.SearchTerm = string.IsNullOrEmpty(searchOptions.SearchTerm) ? string.Empty : searchOptions.SearchTerm;
            var searchTermParam = new SqlParameter("searchTerm", searchOptions.SearchTerm);
            var walletIdParam = new SqlParameter("walletId", searchOptions.WalletId.ToDeobfuscated());
            var dateFromParam = new SqlParameter("dateFrom", searchOptions.DateFrom);
            var dateToParam = new SqlParameter("dateTo", searchOptions.DateTo);
            var batchSizeParam = new SqlParameter("batchSize", searchOptions.BatchSize);
            var toSkipParam = new SqlParameter("toSkip", searchOptions.Page * searchOptions.BatchSize);
            var currentUserIdParam = new SqlParameter("currentUserId", userId);
            var categoryParam = new SqlParameter("category", (int)searchOptions.Category);
            if (searchOptions.Category == ExpenseCategory.All)
            {
                categoryParam.Value = DBNull.Value;
            }
            var typeParam = new SqlParameter("type", (int)searchOptions.Type);
            if (searchOptions.Type == BalanceType.All)
            {
                typeParam.Value = DBNull.Value;
            }

            return await Context.SearchExpenses
                .FromSql(
                    "exec budget_SearchExpenses @searchTerm, @walletId, @dateFrom, @dateTo, @batchSize, @toSkip, @currentUserId, @category, @type",
                    searchTermParam, walletIdParam, dateFromParam, dateToParam, batchSizeParam, toSkipParam, currentUserIdParam,
                    categoryParam, typeParam).ToListAsync();
        }
    }
}
