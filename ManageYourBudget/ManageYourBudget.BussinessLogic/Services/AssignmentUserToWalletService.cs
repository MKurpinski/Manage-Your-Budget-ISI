using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.ExternalAbstractions;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Common;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.Common.Extensions;
using ManageYourBudget.DataAccess.Interfaces;
using ManageYourBudget.DataAccess.Models;
using ManageYourBudget.Dtos.Wallet;
using ManageYourBudget.Dtos.Wallet.Assignment;

namespace ManageYourBudget.BussinessLogic.Services
{
    public class AssignmentUserToWalletService : IAssignmentUserToWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IUserManager _userManager;

        public AssignmentUserToWalletService(IWalletRepository walletRepository, IUserManager userManager)
        {
            _walletRepository = walletRepository;
            _userManager = userManager;
        }

        public async Task<Result> AssignUserToWallet(AssignUserToWalletDto assignUserToWalletDto, string modifierId)
        {
            var validationResult = await Validate(assignUserToWalletDto, modifierId);
            if (!validationResult.Succedeed)
            {
                return validationResult;
            }
            var deobfustacedId = assignUserToWalletDto.WalletId.ToDeobfuscated();
            var userWallet = await _walletRepository.Get(deobfustacedId, assignUserToWalletDto.UserId, true);

            if (userWallet == null)
            {
                return await NewlyAssignUser(deobfustacedId, assignUserToWalletDto.UserId);
            }
            return await ChangeUserAssignment(userWallet, WalletRole.Normal);
        }

        public async Task<Result> UnAssignUserFromWallet(UnAssignUserFromWalletDto assignUserToWalletDto, string modifierId)
        {
            var validationResult = await Validate(assignUserToWalletDto, modifierId);
            if (!validationResult.Succedeed)
            {
                return validationResult;
            }

            var userWallet = await _walletRepository.Get(assignUserToWalletDto.WalletId.ToDeobfuscated(),
                assignUserToWalletDto.UserId);

            if (userWallet == null)
            {
                return Result.Failure();
            }
            return await ChangeUserAssignment(userWallet, WalletRole.InActive);
        }

        public async Task<Result> ChangeUserRole(ChangeUserRoleDto changeUserRoleDto, string modifierId)
        {
            var validationResult = await Validate(changeUserRoleDto, modifierId);
            if (!validationResult.Succedeed)
            {
                return validationResult;
            }

            var userWallet = await _walletRepository.Get(changeUserRoleDto.WalletId.ToDeobfuscated(),
                changeUserRoleDto.UserId);
            if (userWallet == null)
            {
                return Result.Failure();
            }
            return await ChangeUserAssignment(userWallet, changeUserRoleDto.Role.ToEnumValue<WalletRole>());
        }

        private async Task<Result> ChangeUserAssignment(UserWallet userWallet, WalletRole role)
        {
            userWallet.Role = role;
            await _walletRepository.Update(userWallet);
            //TODO Send Email
            return Result.Success();
        }

        private async Task<Result> NewlyAssignUser(int walletId, string userId)
        {
            var userWallet = new UserWallet
            {
                UserId = userId,
                Role = WalletRole.Normal,
                WalletId = walletId
            };
            await _walletRepository.Add(userWallet);
            //TODO Send Email
            return Result.Success();
        }

        private async Task<Result> Validate(BaseAssignmentDto assignUserToWalletDto, string modifierId)
        {
            var userToAdd = await _userManager.GetByAsync(x => x.Id == assignUserToWalletDto.UserId);
            if (userToAdd == null)
            {
                return Result.Failure();
            }

            var userWalletOfModifier = await _walletRepository.Get(assignUserToWalletDto.WalletId.ToDeobfuscated(), modifierId);
            if (userWalletOfModifier == null || userWalletOfModifier.Role != WalletRole.AllPrivileges)
            {
                return Result.Failure();
            }

            return Result.Success();
        }
    }
}
