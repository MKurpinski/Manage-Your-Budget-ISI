using System.Collections.Generic;
using System.Threading.Tasks;
using ManageYourBudget.DataAccess.Models;

namespace ManageYourBudget.DataAccess.Interfaces
{
    public interface IWalletRepository : IRepository
    {
        Task<UserWallet> Add(UserWallet wallet);
        Task<IList<UserWallet>> GetAll(string userId);
        Task<UserWallet> Get(int id, string userId, bool includeInActive = false);
        Task<int> Update<T>(T wallet) where T : Entity;
    }
}
