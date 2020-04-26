using System.Net;
using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Dtos.Wallet.Assignment;
using Microsoft.AspNetCore.Mvc;

namespace ManageYourBudget.Api.Controllers
{
    [Route("api/wallets/users")]
    public class AssignmentUserToWalletController : BaseController
    {
        private readonly IAssignmentUserToWalletService _assignmentUserToWalletService;

        public AssignmentUserToWalletController(IAssignmentUserToWalletService assignmentUserToWalletService)
        {
            _assignmentUserToWalletService = assignmentUserToWalletService;
        }

        [HttpPut("assign")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AssignUserToWallet([FromBody] AssignUserToWalletDto assignUserToWalletDto)
        {
            var result = await _assignmentUserToWalletService.AssignUserToWallet(assignUserToWalletDto, UserId);
            if (!result.Succedeed)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPut("unassign")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UnAssignUserToWallet([FromBody] UnAssignUserFromWalletDto unassignUserToWalletDto)
        {
            var result = await _assignmentUserToWalletService.UnAssignUserFromWallet(unassignUserToWalletDto, UserId);
            if (!result.Succedeed)
            {
                return BadRequest();
            }
            return NoContent();
        }


        [HttpPut("role")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ChangeUserRole([FromBody] ChangeUserRoleDto changeUserRoleDto)
        {
            var result = await _assignmentUserToWalletService.ChangeUserRole(changeUserRoleDto, UserId);
            if (!result.Succedeed)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
