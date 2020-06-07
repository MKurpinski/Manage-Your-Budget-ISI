using System;
using System.Collections.Generic;
using System.Text;

namespace ManageYourBudget.Dtos.Expense
{
    public class ExpenseDto: BaseExpenseDto
    {
        public DateTime Date { get; set; }
    }
}
