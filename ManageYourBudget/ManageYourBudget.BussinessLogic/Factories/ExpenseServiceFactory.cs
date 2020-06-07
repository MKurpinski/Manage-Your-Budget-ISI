using Autofac.Features.Indexed;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Common.Enums;

namespace ManageYourBudget.BussinessLogic.Factories
{
    public class ExpenseServiceFactory: IExpenseServiceFactory
    {
        private readonly IIndex<ExpenseType, IExpenseService> _expenseServices;

        public ExpenseServiceFactory(IIndex<ExpenseType, IExpenseService> expenseServices)
        {
            _expenseServices = expenseServices;
        }

        public IExpenseService Create(ExpenseType type)
        {
            return _expenseServices[type];
        }
    }
}
