using System.Linq;
using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.EventDispatchers.Interfaces;
using ManageYourBudget.BussinessLogic.ExternalAbstractions;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.DataAccess.Models;
using ManageYourBudget.Dtos.Wallet.Assignment;
using ManageYourBudget.Options;
using ManageYourBudget.Shared.Events;
using Microsoft.Extensions.Options;

namespace ManageYourBudget.BussinessLogic.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEventDispatcher<ISendEmailEvent> _emailDispatcher;
        private readonly IUserManager _userManager;
        private readonly PasswordResetOptions _passwordResetOptions;
        private readonly ClientOptions _clientOptions;

        public EmailService(IOptions<PasswordResetOptions> passwordResetOptionsAccessor,
            IUserManager userManager,
            IEventDispatcher<ISendEmailEvent> emailDispatcher,
            IOptions<ClientOptions> clientOptionsAccessor)
        {
            _userManager = userManager;
            _emailDispatcher = emailDispatcher;
            _clientOptions = clientOptionsAccessor.Value;
            _passwordResetOptions = passwordResetOptionsAccessor.Value;
        }

        public void SendResetPasswordEmail(string email, bool canBeReset, string hash = null)
        {
            var resetPasswordMessage = new ResetPasswordMessage
            {
                To = email,
                CanBeResetInternally = canBeReset,
                Subject = "Password reset",
                Link = $"{_passwordResetOptions.Url}/{hash}"
            };
            _emailDispatcher.Dispatch(resetPasswordMessage);
        }

        public void SendWalletArchivedEmail(string userId, Wallet wallet)
        {
            Task.Run(() =>
            {
                var modifier = wallet.UserWallets.FirstOrDefault(x => x.UserId == userId)?.User;
                foreach (var uWallet in wallet.UserWallets.Where(x => x.UserId != userId))
                {
                    var message = new WalletArchivedMessage
                    {
                        To = uWallet.User.Email,
                        Subject = $"Wallet {wallet.Name} archived",
                        By = $"{modifier?.FirstName} {modifier?.LastName}",
                        WalletName = wallet.Name
                    };
                    _emailDispatcher.Dispatch(message);
                }
            });
        }

        public void SendAssignmentEmail(BaseAssignmentDto assignUserToWalletDto,
            bool assigment, string modifierId, string walletName)
        {
            Task.Run(async () =>
            {
                var to = await _userManager.GetByAsync(x => x.Id == assignUserToWalletDto.UserId);
                var modifier = await _userManager.GetByAsync(x => x.Id == modifierId);
                var by = $"{modifier.FirstName} {modifier.LastName}";
                var message = new AssignToWalletMessage
                {
                    To = to.Email,
                    Subject = assigment ? $"{by} shared a wallet with you" : $"{by} unassigned you from wallet {walletName}",
                    By = by,
                    Link = $"{_clientOptions.WalletUrl}/{assignUserToWalletDto.WalletId}"
                };

                _emailDispatcher.Dispatch(assigment ? message : GetUnnassignMessage(message, walletName));
            });
        }

        private UnassignFromWalletMessage GetUnnassignMessage(AssignToWalletMessage message, string walletName)
        {
            return new UnassignFromWalletMessage(message, walletName);
        }
    }
}
