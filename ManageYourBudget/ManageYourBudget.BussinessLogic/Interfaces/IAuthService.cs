using System.Threading.Tasks;
using ManageYourBudget.Common;
using ManageYourBudget.Dtos.Auth;

namespace ManageYourBudget.BussinessLogic.Interfaces
{
    public interface IAuthService: IService
    {
        Task<Result> RegisterUser(LocalRegisterUserDto registerUserDto);
        Task<Result<TokenDto>> Login(LoginUserDto loginUserDto);
    }
}
