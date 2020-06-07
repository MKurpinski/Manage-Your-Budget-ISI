using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ManageYourBudget.Dtos.Expense;

namespace ManageYourBudget.BussinessLogic.Interfaces
{
    public interface ICyclicExpenseService : IExpenseService
    {
        Task<List<CyclicExpenseDto>> GetAllCyclicExpenses(string walletId, string userId);
    }
}
