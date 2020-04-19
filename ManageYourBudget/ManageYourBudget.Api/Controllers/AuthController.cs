using System.Net;
using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Common;
using ManageYourBudget.Dtos.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ManageYourBudget.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController: BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody] LocalRegisterUserDto registerUserDto)
        {
            var registerResult = await _authService.RegisterUser(registerUserDto);

            if (!registerResult.Succedeed)
            {
                return BadRequest(registerResult);
            }
            return NoContent();
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(Result<TokenDto>),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<TokenDto>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var loginResult = await _authService.Login(loginUserDto);

            if (!loginResult.Succedeed)
            {
                return BadRequest(loginResult);
            }
            return Ok(loginResult);
        }
    }
}
