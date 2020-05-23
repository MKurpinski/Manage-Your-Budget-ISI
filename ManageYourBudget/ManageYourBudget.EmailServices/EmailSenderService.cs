using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using FluentEmail.Core;
using Shared.Events;

namespace ManageYourBudget.EmailService
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IFluentEmailFactory _emailClientFactory;

        public EmailSenderService(IFluentEmailFactory emailClientFactory)
        {
            _emailClientFactory = emailClientFactory;
        }

        public void SendEmail<T>(T message) where T : BaseMessage
        {
            var client = _emailClientFactory.Create();
            if (!string.IsNullOrWhiteSpace(message.From))
            {
                client = client.SetFrom(message.From);
            }

            Task.Run(() =>
            {
                client.To(message.To).Subject(message.Subject).UsingTemplateFromEmbedded(
                    $"ManageYourBudget.EmailService.Views.{message.Type}.cshtml", message,
                    GetType().GetTypeInfo().Assembly).Send();
            });
        }

        public void SendEmails<T>(ICollection<T> messages) where T : BaseMessage
        {
            Task.Run(() =>
            {
                foreach (var message in messages)
                {
                    SendEmail(message);
                }
            });
        }
    }
}
