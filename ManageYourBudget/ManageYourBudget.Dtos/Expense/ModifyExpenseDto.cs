using System;
using ManageYourBudget.Dtos.ValidationAttributes;

namespace ManageYourBudget.Dtos.Expense
{
    public class ModifyExpenseDto: BaseModifyExpenseDto
    {

        [DateSmallerThanNow]
        public DateTime Date { get; set; }
        public int? CyclicExpenseId { get; set; }
    }
}
