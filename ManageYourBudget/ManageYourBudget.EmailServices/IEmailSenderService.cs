
using Shared.Events;

namespace ManageYourBudget.EmailService
{
    public interface IEmailSenderService
    {
        void SendEmail<T>(T message) where T : BaseMessage;
    }
}
