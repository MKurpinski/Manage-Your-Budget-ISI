using ManageYourBudget.Shared.Interfaces;

namespace ManageYourBudget.Shared.Events
{
    public interface ISendEmailEvent: IEvent
    {
        string Subject { get; set; }
        string To { get; set; }
        string From { get; set; }
        string Type { get; }
    }
}
