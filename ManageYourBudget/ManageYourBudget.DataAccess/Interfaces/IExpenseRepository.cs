using System.Threading.Tasks;
using ManageYourBudget.DataAccess.Models.Expense;

namespace ManageYourBudget.DataAccess.Interfaces
{
    public interface IExpenseRepository: IRepository
    {
        Task CreateExpense(Expense expense);
        Task<Expense> GetExpense(int id);
        void DeleteExpense(Expense expense);
        int UpdateExpense(Expense expense);
    }
}
