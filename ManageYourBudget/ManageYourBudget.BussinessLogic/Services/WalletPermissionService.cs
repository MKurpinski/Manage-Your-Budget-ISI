using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.Common.Extensions;
using ManageYourBudget.DataAccess.Interfaces;

namespace ManageYourBudget.BussinessLogic.Services
{
    public class WalletPermissionService: IWalletPermissionService
    {
        private readonly IWalletRepository _walletRepository;

        public WalletPermissionService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<bool> HasUserAccess(int walletId, string userId, WalletRole role = WalletRole.Normal)
        {
            var wallet = await _walletRepository.GetWithoutDependencies(walletId, userId);
            if (wallet == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> HasUserAccess(string walletId, string userId, WalletRole role = WalletRole.Normal)
        {
            return await HasUserAccess(walletId.ToDeobfuscated(), userId, role);
        }
    }
}