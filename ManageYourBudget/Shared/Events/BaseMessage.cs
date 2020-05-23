namespace ManageYourBudget.Shared.Events
{
    public abstract class BaseMessage: ISendEmailEvent
    {
        public string Subject { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public abstract string Type { get; }
    }
}
