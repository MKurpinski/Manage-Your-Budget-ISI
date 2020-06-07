using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Common.Enums;

namespace ManageYourBudget.BussinessLogic.Factories
{
    public interface IExpenseServiceFactory: IFactory
    {
        IExpenseService Create(ExpenseType type);
    }
}
