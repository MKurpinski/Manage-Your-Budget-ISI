using System.Net;
using System.Threading.Tasks;

namespace ManageYourBudget.BussinessLogic.Interfaces
{
    public interface ICacheService : IService
    {
        Task SetState(string ip, string state);
        Task<string> GetState(string ip);
    }
}
