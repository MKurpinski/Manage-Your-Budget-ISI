using System;
using System.Threading.Tasks;
using AutoMapper;
using ManageYourBudget.BussinessLogic.ExternalAbstractions;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.Dtos.Auth;
using ManageYourBudget.Dtos.Auth.LoginDataProvider;

namespace ManageYourBudget.BussinessLogic.Providers.LoginData
{
    public class FacebookLoginDataProvider: ILoginDataProvider
    {
        private readonly IFacebookClient _facebookClient;
        private readonly IMapper _mapper;

        public FacebookLoginDataProvider(IFacebookClient facebookClient, IMapper mapper)
        {
            _facebookClient = facebookClient;
            _mapper = mapper;
        }

        public LoginProvider Type => LoginProvider.Facebook;

        public async Task<ExternalRegisterUserDto> GetExternalData(IExternalDataDto externalData)
        {
            if (!(externalData is FacebookExternalDataDto facebookExternalData))
            {
                throw new ArgumentNullException(nameof(facebookExternalData));
            }

            var facebookUserDto = await _facebookClient.GetFacebookUserData(facebookExternalData.AccessToken);
            var result = _mapper.Map<ExternalRegisterUserDto>(facebookUserDto);
            return result;
        }

        public string GetRedirectUrl()
        {
            return string.Empty;
        }

        public string GetLogoutUri()
        {
            return string.Empty;
        }
    }
}
