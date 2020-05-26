using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManageYourBudget.BussinessLogic.ExternalAbstractions;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Common;
using ManageYourBudget.Common.Extensions;
using ManageYourBudget.DataAccess.Models;
using ManageYourBudget.Dtos.Profile;
using ManageYourBudget.Dtos.Search;

namespace ManageYourBudget.BussinessLogic.Services
{
    public class ProfileService: BaseService, IProfileService
    {
        private readonly IUserManager _userManager;
        private readonly IWalletService _walletService;

        public ProfileService(IUserManager userManager, IMapper mapper, IWalletService walletService) : base(mapper)
        {
            _userManager = userManager;
            _walletService = walletService;
        }

        public async Task<Result> ChangePassword(ChangePasswordDto changePasswordDto, string userId)
        {
            return await ProfileModificationAction(ChangePassword, changePasswordDto, userId);
        }

        public async Task<Result> AddPassword(AddPasswordDto addPasswordDto, string userId)
        {
            return await ProfileModificationAction(AddPassword, addPasswordDto, userId);
        }

        public async Task<Result> UpdateProfile(UpdateProfileDto updateProfileDto, string userId)
        {
            return await ProfileModificationAction(UpdateProfile, updateProfileDto, userId);
        }

        public async Task<BaseSearchResults<UserDto>> Search(BaseSearchOptionsDto searchOptions, string userId)
        {
            var userResults = await _userManager.Search(searchOptions.SearchTerm,  userId);
            var searchResult = new BaseSearchResults<UserDto>
            {
                Results = Mapper.Map<List<UserDto>>(userResults)
            };

            return searchResult;
        }

        public async Task<UserDto> GetProfile(string userId)
        {
            var user = await _userManager.GetByAsync(x => x.Id == userId);
            var hasAnyWallet = await _walletService.HasAnyWallet(userId);
            var mapped =  Mapper.Map<UserDto>(user);
            mapped.HasAnyWallet = hasAnyWallet;
            return mapped;
        }

        private async Task<Result> UpdateProfile(UpdateProfileDto updateProfileDto, User user)
        {
            user.Modified = DateTime.UtcNow;
            user.FirstName = updateProfileDto.FirstName;
            user.LastName = updateProfileDto.LastName;
            var identityResult = await _userManager.UpdateAsync(user);
            return identityResult.ToResult();
        }
        private async Task<Result> AddPassword(AddPasswordDto addPasswordDto, User user)
        {
            var identityResult = await _userManager.AddPasswordAsync(user, addPasswordDto.Password);
            return identityResult.ToResult();
        }

        private async Task<Result> ChangePassword(ChangePasswordDto changePasswordDto, User user)
        {
            var identityResult = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.Password);
            return identityResult.ToResult();
        }

        private async Task<Result> ProfileModificationAction<T>(Func<T, User, Task<Result>> modificationAction, T dto, string userId)
        {
            var user = await _userManager.GetByAsync(x => x.Id == userId);
            if (user == null)
            {
                return Result.Failure();
            }
            return await modificationAction(dto, user);
        }
    }
}
