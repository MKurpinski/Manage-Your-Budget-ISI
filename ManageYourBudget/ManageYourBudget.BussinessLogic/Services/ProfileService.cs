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

        public ProfileService(IUserManager userManager, IMapper mapper) : base(mapper)
        {
            _userManager = userManager;
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

        public async Task<PartialSearchResults<UserDto>> Search(BaseSearchOptionsDto searchOptions, string userId)
        {
            var toSkipEntries = searchOptions.Batch * searchOptions.Page;
            var userResults = await _userManager.Search(searchOptions.SearchTerm, toSkipEntries, searchOptions.Batch, userId);
            var searchResult = new PartialSearchResults<UserDto>
            {
                Page = searchOptions.Page + 1,
                IsMore = userResults.Count > searchOptions.Batch,
                Results = Mapper.Map<List<UserDto>>(userResults.Take(searchOptions.Batch))
            };

            return searchResult;
        }

        public async Task<UserDto> GetProfile(string userId)
        {
            var user = await _userManager.GetByAsync(x => x.Id == userId);
            return Mapper.Map<UserDto>(user);
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
