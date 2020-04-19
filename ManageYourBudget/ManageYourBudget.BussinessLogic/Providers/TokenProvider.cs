using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.DataAccess.Models;
using ManageYourBudget.Dtos.Auth;
using ManageYourBudget.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ManageYourBudget.BussinessLogic.Providers
{
    public class TokenProvider:ITokenProvider
    {
        private readonly JwtTokenOptions _tokenOptions;

        public TokenProvider(IOptions<JwtTokenOptions> tokenOptionsAccessor)
        {
            _tokenOptions = tokenOptionsAccessor.Value;
        }

        public JwtSecurityToken GetJwtSecurityToken(User user, LoginProvider loggedWith = LoginProvider.Local,
            string externalAccessToken = null)
        {
            return new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: GetClaims(user, loggedWith, externalAccessToken),
                expires: DateTime.UtcNow.AddMinutes(_tokenOptions.TokenExpirationTimeInMinutes),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Key)), SecurityAlgorithms.HmacSha256)
            );
        }

        public TokenDto CreateTokenResponse(SecurityToken token)
        {
            var tokenResponse = new TokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };
            return tokenResponse;
        }

        private IEnumerable<Claim> GetClaims(User user, LoginProvider loggedWith, string externalAccessToken)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.Typ, loggedWith.ToString()),
            };

            if (loggedWith == LoginProvider.Google)
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.CHash, externalAccessToken));
            }
            return claims;
        }
    }
}
