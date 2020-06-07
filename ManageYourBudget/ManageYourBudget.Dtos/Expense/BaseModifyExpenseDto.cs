using System.ComponentModel.DataAnnotations;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.Dtos.ValidationAttributes;

namespace ManageYourBudget.Dtos.Expense
{
    public class BaseModifyExpenseDto
    {
        [Required]
        public string Name { get; set; }
        public string Place { get; set; }
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        [EnumRestriction((int)ExpenseCategory.All)]
        public ExpenseCategory Category { get; set; }
        public string WalletId { get; set; }
        [EnumRestriction((int)BalanceType.All)]
        public BalanceType Type { get; set; }
    }
}
