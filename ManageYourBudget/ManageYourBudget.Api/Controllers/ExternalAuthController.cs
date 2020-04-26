using System.Net;
using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Common;
using ManageYourBudget.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageYourBudget.Api.Controllers
{
    [Route("api/external")]
    public class ExternalAuthController : Controller
    {
        private readonly IExternalAuthService _externalLoginService;

        public ExternalAuthController(IExternalAuthService externalAuthService)
        {
            _externalLoginService = externalAuthService;
        }

        [HttpPost("facebook")]
        [ProducesResponseType(typeof(Result<TokenDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ExternalLoginFacebook([FromBody] FacebookLoginDto externalLoginDto)
        {
            var result = await _externalLoginService.LoginFacebook(externalLoginDto);
            return HandleExternalLoginResult(result);
        }

        [HttpPost("google")]
        [ProducesResponseType(typeof(Result<TokenDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ExternalLoginGoogle([FromBody] GoogleLoginDto externalLoginDto)
        {
            var userIp = Request.HttpContext.Connection.RemoteIpAddress;
            var result = await _externalLoginService.LoginGoogle(externalLoginDto, userIp);
            return HandleExternalLoginResult(result);
        }

        [HttpGet("google")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetGoogleRedirectLogin()
        {
            var userIp = Request.HttpContext.Connection.RemoteIpAddress;
            var redirectUri = _externalLoginService.GetRedirectUrl(userIp);
            return Ok(redirectUri);
        }

        [Authorize]
        [HttpPost, Route("logout")]
        [ProducesResponseType(typeof(LogoutDto), (int)HttpStatusCode.OK)]
        public IActionResult Logout()
        {
            var result = _externalLoginService.Logout(User);
            return Ok(result);
        }

        private IActionResult HandleExternalLoginResult(Result<TokenDto> result)
        {
            if (!result.Succedeed)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
