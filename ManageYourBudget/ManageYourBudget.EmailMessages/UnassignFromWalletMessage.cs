namespace ManageYourBudget.EmailMessages
{
    public class UnassignFromWalletMessage : AssignToWalletMessage
    {
        public override string Type => nameof(UnassignFromWalletMessage);
        public string WalletName { get; set; }
    }
}
