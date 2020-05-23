namespace ManageYourBudget.Shared.Events
{
    public class ResetPasswordMessage : BaseMessage
    {
        public string Link { get; set; }
        public bool CanBeResetInternally { get; set; }
        public override string Type => nameof(ResetPasswordMessage);
    }
}
