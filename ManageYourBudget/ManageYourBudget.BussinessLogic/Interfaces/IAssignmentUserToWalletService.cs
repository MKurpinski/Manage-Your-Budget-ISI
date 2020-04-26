using System.Threading.Tasks;
using ManageYourBudget.Common;
using ManageYourBudget.Dtos.Wallet;
using ManageYourBudget.Dtos.Wallet.Assignment;

namespace ManageYourBudget.BussinessLogic.Interfaces
{
    public interface IAssignmentUserToWalletService: IService
    {
        Task<Result> AssignUserToWallet(AssignUserToWalletDto assignUserToWalletDto, string modifierId);
        Task<Result> UnAssignUserFromWallet(UnAssignUserFromWalletDto assignUserToWalletDto, string modifierId);
        Task<Result> ChangeUserRole(ChangeUserRoleDto changeUserRoleDto, string modifierId);
    }
}
