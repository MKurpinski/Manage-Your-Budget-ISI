using ManageYourBudget.Common.Enums;

namespace ManageYourBudget.DataAccess.Models
{
    public class Wallet: Entity
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string Name { get; set; }
        public bool Archived { get; set; }
        public WalletCategory Category { get; set; }
        public SupportedCurrencies DefaultCurrency { get; set; }
    }
}
