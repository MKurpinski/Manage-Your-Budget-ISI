using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using ManageYourBudget.BussinessLogic.ExternalAbstractions;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Common.Constants;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.Dtos.Auth;
using ManageYourBudget.Dtos.Auth.LoginDataProvider;

namespace ManageYourBudget.BussinessLogic.Providers.LoginData
{
    public class GoogleLoginDataProvider : ILoginDataProvider
    {
        private readonly IGoogleClient _googleClient;
        private readonly ICacheService _cacheService;

        public GoogleLoginDataProvider(IGoogleClient googleClient, ICacheService cacheService)
        {
            _googleClient = googleClient;
            _cacheService = cacheService;
        }

        public LoginProvider Type => LoginProvider.Google;
        public async Task<ExternalRegisterUserDto> GetExternalData(IExternalDataDto externalData)
        {
            if (!(externalData is GoogleExternalDataDto googleExternalData))
            {
                throw new ArgumentNullException(nameof(googleExternalData));
            }
            var tokenResponse = await _googleClient.RequestAuthorizationCodeAsync(googleExternalData.LoginDto.Code);

            var validatedToken = await ValidateOpenIdToken(tokenResponse, googleExternalData.LoginDto.State, googleExternalData.Ip);

            if (!validatedToken)
            {
                return null;
            }
            return await CreateExternalRegisterUserDto(tokenResponse);
        }

        public string GetRedirectUrl(string ip)
        {
            var state = Guid.NewGuid().ToString("N");
            _cacheService.SetState(ip, state);
            return _googleClient.GetRedirectUrl(state);
        }

        public string GetLogoutUri()
        {
            return _googleClient.GetLogoutUri();
        }

        private async Task<ExternalRegisterUserDto> CreateExternalRegisterUserDto(TokenResponse tokenResponse)
        {
            var externalRegisterUserDto = await GetUserInfo(tokenResponse.AccessToken);

            if (externalRegisterUserDto == null)
            {
                return null;
            }

            externalRegisterUserDto.AccessToken = tokenResponse.AccessToken;
            return externalRegisterUserDto;
        }

        private async Task<ExternalRegisterUserDto> GetUserInfo(string accessToken)
        {
            var userInfo = await _googleClient.GetUserInfo(accessToken);
            return !userInfo.IsError ? GetUserFromGoogle(userInfo.Claims.ToList()) : null;
        }

        private static ExternalRegisterUserDto GetUserFromGoogle(ICollection<Claim> claims)
        {
            return new ExternalRegisterUserDto
            {
                Email = claims.FirstOrDefault(x => x.Type == GoogleValues.Email)?.Value,
                FirstName = claims.FirstOrDefault(x => x.Type == GoogleValues.FirstName)?.Value,
                LastName = claims.FirstOrDefault(x => x.Type == GoogleValues.LastName)?.Value,
                PictureSrc = claims.FirstOrDefault(x => x.Type == GoogleValues.PictureSrc)?.Value
            };
        }

        private async Task<bool> ValidateOpenIdToken(TokenResponse tokenResponse, string state, IPAddress ip)
        {
            if (tokenResponse.IsError ||
                !state.Equals(await _cacheService.GetState(ip.ToString()), StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            return await _googleClient.ValidateToken(tokenResponse.AccessToken);
        }
    }
}
