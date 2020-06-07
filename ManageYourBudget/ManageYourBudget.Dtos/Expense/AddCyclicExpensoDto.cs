using System;
using ManageYourBudget.Common.Enums;


namespace ManageYourBudget.Dtos.Expense
{
    public class AddCyclicExpenseDto: BaseModifyExpenseDto
    {
        public DateTime StartingFrom { get; set; }
        public CyclicExpensePeriodType PeriodType { get; set; }
    }
}
