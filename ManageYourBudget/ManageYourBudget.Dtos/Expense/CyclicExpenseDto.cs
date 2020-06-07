using System;

namespace ManageYourBudget.Dtos.Expense
{
    public class CyclicExpenseDto: BaseExpenseDto
    {
        public DateTime NextApplyingDate { get; set; }
        public DateTime StartingFrom { get; set; }
        public string PeriodType { get; set; }
    }
}
