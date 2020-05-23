using System.Threading.Tasks;

namespace ManageYourBudget.Shared.Interfaces
{
    public interface IEventHandler<T> where T : IEvent
    {
        Task Handle(T @event);
    }
}
