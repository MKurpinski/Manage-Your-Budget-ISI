using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using ManageYourBudget.Common;
using ManageYourBudget.Dtos.Auth;

namespace ManageYourBudget.BussinessLogic.Interfaces
{
    public interface IExternalAuthService: IService
    {
        Task<Result<TokenDto>> LoginFacebook(FacebookLoginDto facebookLoginDto);
        Task<Result<TokenDto>> LoginGoogle(GoogleLoginDto googleLoginDto, IPAddress ip);
        string GetRedirectUrl(IPAddress userIp);
        LogoutDto Logout(ClaimsPrincipal claimsPrincipal);
    }
}
