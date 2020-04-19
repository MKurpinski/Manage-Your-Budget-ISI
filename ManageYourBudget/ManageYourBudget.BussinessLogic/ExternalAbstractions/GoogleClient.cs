using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using IdentityModel.Client;
using ManageYourBudget.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace ManageYourBudget.BussinessLogic.ExternalAbstractions
{
    public class GoogleClient : IGoogleClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly GoogleOptions _googleOptions;

        public GoogleClient(IOptions<GoogleOptions> googleOptionsAccessor, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _googleOptions = googleOptionsAccessor.Value;
        }

        public async Task<TokenResponse> RequestAuthorizationCodeAsync(string code)
        {
            using (var client = new TokenClient(_googleOptions.TokenEndpoint, _googleOptions.ClientId, _googleOptions.ClientSecret))
            {
                return await client.RequestAuthorizationCodeAsync(code, _googleOptions.RedirectUrl);
            }
        }

        public async Task<UserInfoResponse> GetUserInfo(string token)
        {
            var client = new UserInfoClient(_googleOptions.UserInfoEndpoint);
            return await client.GetAsync(token);
        }

        public async Task<bool> ValidateToken(string token)
        {
            var rsa = new RSACryptoServiceProvider();
            var handler = new JwtSecurityTokenHandler();

            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            rsa.ImportParameters(await CreateRsaParameters(jsonToken?.Header.Kid));
            var parameters = CreateTokenValidatorParameters(rsa);
            try
            {
                handler.ValidateToken(token, parameters, out _);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public string GetRedirectUrl(string state)
        {
            var request = new AuthorizeRequest(_googleOptions.AuthorizeEndpoint);

            var url = request.CreateAuthorizeUrl(
                clientId: _googleOptions.ClientId,
                responseType: _googleOptions.ResponseType,
                scope: _googleOptions.Scope,
                redirectUri: _googleOptions.RedirectUrl,
                state: state);

            return url;
        }

        public string GetLogoutUri()
        {
            return _googleOptions.LogOutEndpoint;
        }

        private TokenValidationParameters CreateTokenValidatorParameters(RSA rsa)
        {
            var parameters = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                RequireSignedTokens = true,
                ValidateLifetime = true,
                IssuerSigningKey = new RsaSecurityKey(rsa),
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidIssuer = _googleOptions.ValidIssuer
            };

            return parameters;
        }

        private async Task<RSAParameters> CreateRsaParameters(string kid)
        {
            var key = await GetJwksInfo(kid);
            var keyExponent = GetKeyInBase64Format(key.KeyExponent);
            var keyModules = GetKeyInBase64Format(key.KeyModules);
            return new RSAParameters
            {
                Modulus = Convert.FromBase64String(keyModules),
                Exponent = Convert.FromBase64String(keyExponent)
            };
        }

        private static string GetKeyInBase64Format(string keyToFormat)
        {
            var key = keyToFormat.Replace('-', '+').Replace('_', '/');
            var formattedKeyPart = key.PadRight(key.Length + (4 - key.Length % 4) % 4, '=');

            return formattedKeyPart;
        }

        private async Task<JwksKey> GetJwksInfo(string kid)
        {
            using (var httpClient = _clientFactory.CreateClient())
            {
                var response = await httpClient.GetAsync(_googleOptions.JwksUrl);
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<JwksData>(json);
                return data.Keys.FirstOrDefault(x => x.Kid == kid) ?? data.Keys.FirstOrDefault();
            }
        }

        private class JwksData
        {
            public IList<JwksKey> Keys { get; set; }
        }
        private class JwksKey
        {
            [JsonProperty("e")]
            public string KeyExponent { get; set; }
            [JsonProperty("n")]
            public string KeyModules { get; set; }
            public string Kid { get; set; }
        }
    }

    public interface IGoogleClient: IExternalAbstraction
    {
        Task<TokenResponse> RequestAuthorizationCodeAsync(string code);
        Task<UserInfoResponse> GetUserInfo(string token);
        Task<bool> ValidateToken(string token);
        string GetRedirectUrl(string state);
        string GetLogoutUri();
    }
}
