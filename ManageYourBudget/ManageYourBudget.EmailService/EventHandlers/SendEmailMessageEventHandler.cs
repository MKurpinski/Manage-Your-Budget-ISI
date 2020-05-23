using System;
using System.Reflection;
using System.Threading.Tasks;
using FluentEmail.Core;
using ManageYourBudget.Shared.Events;
using ManageYourBudget.Shared.Interfaces;

namespace ManageYourBudget.EmailService.EventHandlers
{
    public class SendMessageEventHandler : IEventHandler<ISendEmailEvent>
    {
        private IFluentEmail _client;

        public SendMessageEventHandler(IFluentEmail client)
        {
            _client = client;
        }

        public Task Handle(ISendEmailEvent message)
        {
            return Task.Run(() =>
            {
                if (!string.IsNullOrWhiteSpace(message.From))
                {
                    _client = _client.SetFrom(message.From);
                }
                _client.To(message.To).Subject(message.Subject).UsingTemplateFromEmbedded(
                    $"ManageYourBudget.EmailService.Views.{message.Type}.cshtml", message,
                    GetType().GetTypeInfo().Assembly).Send();
            });
        }
    }
}