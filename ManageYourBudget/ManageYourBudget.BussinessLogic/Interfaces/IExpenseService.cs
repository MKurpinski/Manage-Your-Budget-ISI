using System.Threading.Tasks;
using ManageYourBudget.Common;
using ManageYourBudget.Dtos.Expense;

namespace ManageYourBudget.BussinessLogic.Interfaces
{
    public interface IExpenseService: IService
    {
        Task<Result<BaseExpenseDto>> CreateExpense(BaseModifyExpenseDto modifyExpenseDto, string userId);
        Task<Result> DeleteExpense(int expenseId, string userId);
        Task<Result> UpdateExpense(BaseModifyExpenseDto editExpenseDto, int expenseId, string userId);
    }
}
