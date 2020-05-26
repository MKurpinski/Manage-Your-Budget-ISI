namespace ManageYourBudget.Shared.Events
{
    public class UnassignFromWalletMessage : AssignToWalletMessage
    {
        public UnassignFromWalletMessage()
        {
            
        }

        public UnassignFromWalletMessage(AssignToWalletMessage message, string walletName)
        {
            From = message.From;
            To = message.To;
            By = message.By;
            Link = message.Link;
            Subject = message.Subject;
            WalletName = walletName;
        }
        public override string Type => nameof(UnassignFromWalletMessage);
        public string WalletName { get; set; }
    }
}
