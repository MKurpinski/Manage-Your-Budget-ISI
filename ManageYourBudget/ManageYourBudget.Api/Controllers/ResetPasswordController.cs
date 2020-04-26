using System.Net;
using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Dtos.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ManageYourBudget.Api.Controllers
{
    [Route("api/reset")]
    public class ResetPasswordController: BaseController
    {
        private readonly IPasswordResetService _passwordResetService;
        public ResetPasswordController(IPasswordResetService passwordResetService)
        {
            _passwordResetService = passwordResetService;
        }

        [HttpPost, Route("start")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> StartResetPasswordFlow([FromBody] StartResetPasswordFlowDto startResetPasswordFlowDto)
        {
            await _passwordResetService.StartResetPasswordFlow(startResetPasswordFlowDto);
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var result = await _passwordResetService.ResetPassword(resetPasswordDto);
            if (!result.Succedeed)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPost, Route("validate")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ValidatePasswordResetHash(string hash)
        {
            var validationHashResult = await _passwordResetService.ValidatePasswordResetHash(hash);
            if (!validationHashResult.Succedeed)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
