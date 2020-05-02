using ManageYourBudget.DataAccess.Models;
using ManageYourBudget.Dtos.Wallet.Assignment;

namespace ManageYourBudget.BussinessLogic.Interfaces
{
    public interface IEmailService: IService
    {
        void SendResetPasswordEmail(string email, bool canBeReset, string hash = null);
        void SendWalletArchivedEmail(string userId, Wallet wallet);
        void SendAssignmentEmail(BaseAssignmentDto assignUserToWalletDto, bool assigment, string modifierId, string walletName);
    }
}
