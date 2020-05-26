namespace ManageYourBudget.Dtos.Wallet
{
    public class BaseWalletDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Category { get; set; }
        public bool Favorite { get; set; }
        public string DefaultCurrency { get; set; }
    }
}
