namespace ManageYourBudget.Shared.Events
{
    public class UnassignFromWalletMessage : AssignToWalletMessage
    {
        public override string Type => nameof(UnassignFromWalletMessage);
        public string WalletName { get; set; }
    }
}
