using System;
using System.Collections.Generic;
using System.Text;
using ManageYourBudget.Common.Enums;

namespace ManageYourBudget.Dtos.Expense
{
    public class ExpenseSearchResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public decimal Price { get; set; }
        public ExpenseCategory Category { get; set; }
        public BalanceType Type { get; set; }
        public DateTime Date { get; set; }
        public DateTime Created { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedByLastName { get; set; }
        public string CreatedByEmail { get; set; }
        public string CreatedByPictureSrc { get; set; }
        public int Total { get; set; }
    }
}
