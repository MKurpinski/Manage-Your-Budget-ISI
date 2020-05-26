using System;
using System.Collections.Generic;
using System.Text;

namespace ManageYourBudget.DataAccess.Models.Expense
{
    public class Expense: BaseExpense
    {
        public DateTime Date { get; set; }
        public int? CyclicExpenseId { get; set; }
        public CyclicExpense CyclicExpense { get; set; }
    }
}
