namespace ManageYourBudget.Shared.Events
{
    public class WalletArchivedMessage: BaseMessage
    {
        public override string Type => nameof(WalletArchivedMessage);
        public string By { get; set; }
        public string WalletName { get; set; }
    }
}
