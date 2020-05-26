using System.Threading.Tasks;
using ManageYourBudget.Common;
using ManageYourBudget.Dtos.Profile;
using ManageYourBudget.Dtos.Search;

namespace ManageYourBudget.BussinessLogic.Interfaces
{
    public interface IProfileService: IService
    {
        Task<Result> ChangePassword(ChangePasswordDto changePasswordDto, string userId);
        Task<Result> AddPassword(AddPasswordDto addPasswordDto, string userId);
        Task<UserDto> GetProfile(string userId);
        Task<Result> UpdateProfile(UpdateProfileDto updateProfileDto, string userId);
        Task<BaseSearchResults<UserDto>> Search(BaseSearchOptionsDto searchOptions, string userId);
    }
}
