namespace ManageYourBudget.EmailMessages
{
    public abstract class BaseMessage
    {
        public string Subject { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public abstract string Type { get; }
    }
}
