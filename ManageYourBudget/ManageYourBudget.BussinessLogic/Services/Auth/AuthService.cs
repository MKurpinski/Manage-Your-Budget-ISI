using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ManageYourBudget.BussinessLogic.ExternalAbstractions;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.BussinessLogic.Providers;
using ManageYourBudget.Common;
using ManageYourBudget.Common.Extensions;
using ManageYourBudget.DataAccess.Models;
using ManageYourBudget.Dtos.Auth;

namespace ManageYourBudget.BussinessLogic.Services.Auth
{
    public class AuthService: BaseAuthService, IAuthService
    {
        public AuthService(IMapper mapper, IUserManager userManager, ITokenProvider tokenProvider): base(mapper, userManager, tokenProvider)
        {
        }

        public async Task<Result> RegisterUser(LocalRegisterUserDto registerUserDto)
        {
            var user = Mapper.Map<User>(registerUserDto);
            var registrationResult = await UserManager.CreateAsync(user, registerUserDto.Password);
            return new Result
            {
               Succedeed = registrationResult.Succeeded,
               Errors = registrationResult.Succeeded ? null : new Dictionary<string, string> { { nameof(registerUserDto.Email).ToLower(), "Email is already taken" } }
            };
        }

        public async Task<Result<TokenDto>> Login(LoginUserDto loginUserDto)
        {
            var user = await UserManager.GetByAsync(x => x.Email == loginUserDto.Email);
            var userAuthorizationStatus = UserManager.VerifyPasswordHash(user, loginUserDto.Password);
            if (!userAuthorizationStatus.Succedeed)
            {
                return CreateInvalidCredentialsResult();
            }
            return CreateTokenResponse(user);
        }

        private static Result<TokenDto> CreateInvalidCredentialsResult()
        {
            return Result<TokenDto>.Failure(new Dictionary<string, string>()
            {
                {"_error", "Email or password is incorrect"}
            });
        }
    }
}
