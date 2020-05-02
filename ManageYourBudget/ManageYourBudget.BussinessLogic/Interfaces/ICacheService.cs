using System.Net;
using System.Threading.Tasks;

namespace ManageYourBudget.BussinessLogic.Interfaces
{
    public interface ICacheService : IService
    {
        Task SetState(string ip, string state);
        Task<string> GetState(string ip);
        Task<T> Get<T>(string key);
        Task Set<T>(string key, T value);
    }
}
