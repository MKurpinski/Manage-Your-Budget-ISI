using System.Collections.Generic;
using System.Threading.Tasks;
using ManageYourBudget.DataAccess.Models.Expense;
using ManageYourBudget.Dtos.Expense;
using ManageYourBudget.Dtos.Search;

namespace ManageYourBudget.DataAccess.Interfaces
{
    public interface IExpenseRepository: IRepository
    {
        Task CreateExpense(Expense expense);
        Task<Expense> GetExpense(int id);
        void DeleteExpense(Expense expense);
        int UpdateExpense(Expense expense);
        Task<List<ExpenseSearchResult>> Search(ExpenseSearchOptionsDto searchOptions, string userId);
    }
}
