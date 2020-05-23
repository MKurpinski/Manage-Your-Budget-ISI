using ManageYourBudget.BussinessLogic.EventDispatchers.Interfaces;
using ManageYourBudget.Shared.Events;
using RawRabbit;

namespace ManageYourBudget.BussinessLogic.EventDispatchers
{
    public class SendMessageEventDispatcher : IEventDispatcher<ISendEmailEvent>
    {
        private readonly IBusClient _client;
        public SendMessageEventDispatcher(IBusClient client)
        {
            _client = client;
        }
        public void Dispatch(ISendEmailEvent @event)
        {
            _client.PublishAsync(@event);
        }
    }
}