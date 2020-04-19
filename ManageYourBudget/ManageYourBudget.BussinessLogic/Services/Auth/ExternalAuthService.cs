using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ManageYourBudget.BussinessLogic.ExternalAbstractions;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.BussinessLogic.Providers;
using ManageYourBudget.BussinessLogic.Providers.LoginData;
using ManageYourBudget.Common;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.DataAccess.Models;
using ManageYourBudget.Dtos.Auth;
using ManageYourBudget.Dtos.Auth.LoginDataProvider;

namespace ManageYourBudget.BussinessLogic.Services.Auth
{
    public class ExternalAuthService: BaseAuthService, IExternalAuthService
    {
        private readonly ILoginDataProviderFactory _loginDataProviderFactory;

        public ExternalAuthService(ILoginDataProviderFactory loginDataProviderFactory,
            ITokenProvider tokenProvider,
            IUserManager userManager,
            IMapper mapper) : base(mapper, userManager, tokenProvider)
        {
            _loginDataProviderFactory = loginDataProviderFactory;
        }

        public async Task<Result<TokenDto>> LoginFacebook(FacebookLoginDto facebookLoginDto)
        {
            var loginProvider = _loginDataProviderFactory.Create(LoginProvider.Facebook);
            var userData = await loginProvider.GetExternalData(new FacebookExternalDataDto{AccessToken = facebookLoginDto.AccessToken});
            return await Login(userData, LoginProvider.Facebook);
        }

        public string GetRedirectUrl()
        {
            return _loginDataProviderFactory.Create(LoginProvider.Google).GetRedirectUrl();
        }

        public LogoutDto Logout(ClaimsPrincipal claimsPrincipal)
        {
            var provider = claimsPrincipal.FindFirst(x => x.Type == JwtRegisteredClaimNames.Typ);

            if (!Enum.TryParse(provider?.Value, out LoginProvider loginProvider) || loginProvider != LoginProvider.Google)
            {
                return new LogoutDto { ExternallyLoggedOut = false };
            }

            var uri = _loginDataProviderFactory.Create(LoginProvider.Google).GetLogoutUri();

            return new LogoutDto
            {
                ExternallyLoggedOut = true,
                Uri = uri
            };
        }

        public async Task<Result<TokenDto>> LoginGoogle(GoogleLoginDto googleLoginDto)
        {
            var loginProvider = _loginDataProviderFactory.Create(LoginProvider.Google);
            var userData = await loginProvider.GetExternalData(new GoogleExternalDataDto { LoginDto = googleLoginDto});
            return await Login(userData, LoginProvider.Google);
        }

        private async Task<Result<TokenDto>> Login(ExternalRegisterUserDto externalUser, LoginProvider loginProvider)
        {
            var user = await UserManager.GetByAsync(x => x.Email == externalUser.Email);

            if (user == null)
            {
                user = await RegisterUserFromExternalService(externalUser, loginProvider);
            }

            if (user == null)
            {
                return Result<TokenDto>.Failure();
            }

            return CreateTokenResponse(user, loginProvider, externalUser.AccessToken);
        }

        private async Task<User> RegisterUserFromExternalService(ExternalRegisterUserDto externalUser, LoginProvider loginProvider)
        {
            var user = Mapper.Map<User>(externalUser);
            user.RegisteredWith = loginProvider;
            var result = await UserManager.CreateAsync(user);
            return result.Succeeded ? user : null;
        }
    }
}
