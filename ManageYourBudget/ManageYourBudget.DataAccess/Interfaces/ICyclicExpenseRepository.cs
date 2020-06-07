using System.Collections.Generic;
using System.Threading.Tasks;
using ManageYourBudget.DataAccess.Models.Expense;

namespace ManageYourBudget.DataAccess.Interfaces
{
    public interface ICyclicExpenseRepository : IRepository
    {
        Task CreateExpense(CyclicExpense expense);
        Task<CyclicExpense> GetExpense(int id);
        Task<List<CyclicExpense>> GetAll(int walletId);
        Task<List<CyclicExpense>> GetAll(int page, int batchSize);
        void DeleteExpense(CyclicExpense expense);
        int UpdateExpense(CyclicExpense expense);
    }
}
