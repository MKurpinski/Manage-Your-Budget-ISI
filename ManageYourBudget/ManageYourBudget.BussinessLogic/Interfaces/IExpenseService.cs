using System.Threading.Tasks;
using ManageYourBudget.Common;
using ManageYourBudget.Dtos.Expense;

namespace ManageYourBudget.BussinessLogic.Interfaces
{
    public interface IExpenseService: IService
    {
        Task<Result> CreateExpense(ModifyExpenseDto modifyExpenseDto, string userId);
        Task<Result> DeleteExpense(int expenseId, string userId);
        Task<Result> UpdateExpense(ModifyExpenseDto editExpenseDto, int expenseId, string userId);
    }
}
