using System;
using System.ComponentModel.DataAnnotations;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.Dtos.ValidationAttributes;

namespace ManageYourBudget.Dtos.Expense
{
    public class ModifyExpenseDto
    {
        [Required]
        public string Name { get; set; }
        public string Place { get; set; }
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string WalletId { get; set; }
        [DateSmallerThanNow]
        public DateTime Date { get; set; }
        public ExpenseType Type { get; set; }
    }
}
