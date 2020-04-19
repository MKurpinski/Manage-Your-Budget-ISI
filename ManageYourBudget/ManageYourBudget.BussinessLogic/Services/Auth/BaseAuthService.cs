using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ManageYourBudget.BussinessLogic.ExternalAbstractions;
using ManageYourBudget.BussinessLogic.Providers;
using ManageYourBudget.Common;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.DataAccess.Models;
using ManageYourBudget.Dtos.Auth;

namespace ManageYourBudget.BussinessLogic.Services.Auth
{
    public class BaseAuthService: BaseService
    {
        protected readonly IUserManager UserManager;
        protected readonly ITokenProvider TokenProvider;

        public BaseAuthService(IMapper mapper, IUserManager userManager, ITokenProvider tokenProvider) : base(mapper)
        {
            UserManager = userManager;
            TokenProvider = tokenProvider;
        }

        protected Result<TokenDto> CreateTokenResponse(User user, LoginProvider loggedWith = LoginProvider.Local,
            string externalToken = null)
        {
            var token = TokenProvider.GetJwtSecurityToken(user, loggedWith, externalToken);
            var tokenResponse = TokenProvider.CreateTokenResponse(token);
            return Result<TokenDto>.Success(tokenResponse);
        }
    }
}
