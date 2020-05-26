using System.Collections.Generic;
using System.Threading.Tasks;
using ManageYourBudget.Common;
using ManageYourBudget.Dtos.Wallet;

namespace ManageYourBudget.BussinessLogic.Interfaces
{
    public interface IWalletService: IService
    {
        Task<Result<string>> CreateWallet(AddWalletDto addWalletDto, string userId);
        Task<List<BaseWalletDto>> GetWallets(string userId);
        Task<bool> HasAnyWallet(string userId);
        Task<Result<ExtendedWalletDto>> GetWallet(string id, string userId);
        Task<Result> UpdateWallet(UpdateWalletDto updateWalletDto, string id, string userId);
        Task<Result> StarWallet(string id, string userId);
        Task<Result> ArchiveWallet(string id, string userId);
    }
}
