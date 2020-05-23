using System;
using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.ExternalAbstractions;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Common;
using ManageYourBudget.DataAccess.Models;
using ManageYourBudget.Dtos.Auth;
using ManageYourBudget.Options;
using Microsoft.Extensions.Options;

namespace ManageYourBudget.BussinessLogic.Services
{
    public class PasswordResetService: IPasswordResetService
    {
        private readonly IUserManager _userManager;
        private readonly IEmailService _emailService;
        private readonly PasswordResetOptions _passwordResetOptions;

        public PasswordResetService(IUserManager userManager, IOptions<PasswordResetOptions> passwordResetOptionsAccessor, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
            _passwordResetOptions = passwordResetOptionsAccessor.Value;
        }

        public async Task StartResetPasswordFlow(StartResetPasswordFlowDto startResetPasswordFlowDto)
        {
            var user = await _userManager.GetByAsync(x => x.Email == startResetPasswordFlowDto.Email);
            if (user == null)
            {
                return;
            }

            var hash = RandomHashGenerator.RandomHash;
            if (user.HasLocalAccount())
            {
                await SaveUserResetPasswordData(user, hash);
            }
            _emailService.SendResetPasswordEmail(user.Email, user.HasLocalAccount(), hash);
        }

        public async Task<Result> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await GetUserByPasswordResetHash(resetPasswordDto.Hash);
            if (user == null)
            {
                return Result.Failure();
            }
            await ResetPassword(resetPasswordDto, user);
            return Result.Success();
        }

        private async Task ResetPassword(ResetPasswordDto resetPasswordDto, User user)
        {
            user.PasswordHash = _userManager.HashPassword(user, resetPasswordDto.Password);
            user.ResetPasswordHash = null;
            user.ResetPasswordHashExpirationTime = null;
            await _userManager.UpdateAsync(user);
        }

        public async Task<Result> ValidatePasswordResetHash(string hash)
        {
            var user = await GetUserByPasswordResetHash(hash);
            return user == null ? Result.Failure() : Result.Success();
        }

        private async Task SaveUserResetPasswordData(User user, string hash)
        {
            user.ResetPasswordHashExpirationTime = DateTime.UtcNow.AddHours(_passwordResetOptions.HashLifetimeInHours);
            user.ResetPasswordHash = hash;
            await _userManager.UpdateAsync(user);
        }

        private async Task<User> GetUserByPasswordResetHash(string hash)
        {
            return await _userManager.GetByAsync(x =>
                x.ResetPasswordHash == hash && x.ResetPasswordHashExpirationTime >= DateTime.UtcNow);
        }
    }
}
