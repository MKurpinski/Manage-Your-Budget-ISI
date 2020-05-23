using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ManageYourBudget.Common;
using ManageYourBudget.Dtos.Auth;
using ManageYourBudget.Dtos.Auth.LoginDataProvider;
using ManageYourBudget.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ManageYourBudget.BussinessLogic.ExternalAbstractions
{
    public class FacebookClient : IFacebookClient
    {
        private readonly HttpClient _httpClient;
        private readonly FacebookOptions _facebookOptions;

        public FacebookClient(IOptions<FacebookOptions> facebookOptionsAccessor, IHttpClientFactory clientFactory)
        {
            _facebookOptions = facebookOptionsAccessor.Value;
            _httpClient = clientFactory.CreateClient();
            InitializeClient();
        }

        public async Task<FacebookUserDto> GetFacebookUserData(string accessToken)
        {
            var response = await _httpClient.GetAsync($"{_facebookOptions.BaseUrl}{_facebookOptions.ProfileEndpoint}?access_token={accessToken}&{_facebookOptions.Permissions}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var stringResult = await response.Content.ReadAsStringAsync();

            var facebookUserResult = JsonConvert.DeserializeObject<FacebookUserDto>(stringResult);
            return facebookUserResult;
        }

        private void InitializeClient()
        {
            _httpClient.BaseAddress = new Uri(_facebookOptions.BaseUrl);
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }

    public interface IFacebookClient: IExternalAbstraction
    {
        Task<FacebookUserDto> GetFacebookUserData(string accessToken);
    }
}
