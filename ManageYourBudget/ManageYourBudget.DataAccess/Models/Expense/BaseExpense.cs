using ManageYourBudget.Common.Enums;

namespace ManageYourBudget.DataAccess.Models.Expense
{
    public abstract class BaseExpense: Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public decimal Price { get; set; }
        public ExpenseCategory Category { get; set; }
        public Wallet Wallet { get; set; }
        public int WalletId { get; set; }
        public string ModifiedById { get; set; }
        public User ModifiedBy { get; set; }
        public string CreatedById { get; set; }
        public User CreatedBy { get; set; }
    }
}
