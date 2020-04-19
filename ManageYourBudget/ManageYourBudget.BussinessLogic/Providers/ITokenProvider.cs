using System.IdentityModel.Tokens.Jwt;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.DataAccess.Models;
using ManageYourBudget.Dtos.Auth;
using Microsoft.IdentityModel.Tokens;

namespace ManageYourBudget.BussinessLogic.Providers
{
    public interface ITokenProvider: IProvider
    {
        JwtSecurityToken GetJwtSecurityToken(User user, LoginProvider loggedWith = LoginProvider.Local,
            string externalAccessToken = null);

        TokenDto CreateTokenResponse(SecurityToken token);
    }
}
