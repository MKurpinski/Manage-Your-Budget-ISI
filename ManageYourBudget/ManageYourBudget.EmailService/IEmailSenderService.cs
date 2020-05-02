using ManageYourBudget.EmailMessages;

namespace ManageYourBudget.EmailService
{
    public interface IEmailSenderService
    {
        void SendEmail<T>(T message) where T : BaseMessage;
    }
}
