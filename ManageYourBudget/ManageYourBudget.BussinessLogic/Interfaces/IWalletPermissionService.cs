using System.Threading.Tasks;
using ManageYourBudget.Common.Enums;

namespace ManageYourBudget.BussinessLogic.Interfaces
{
    public interface IWalletPermissionService: IService
    {
        Task<bool> HasUserAccess(int walletId ,string userId, WalletRole role = WalletRole.Normal);
        Task<bool> HasUserAccess(string walletId ,string userId, WalletRole role = WalletRole.Normal);
    }
}
