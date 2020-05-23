using ManageYourBudget.Shared.Interfaces;

namespace ManageYourBudget.BussinessLogic.EventDispatchers.Interfaces
{
    public interface IEventDispatcher<T> where T : IEvent
    {
        void Dispatch(T @event);
    }
}