using System.Net;
using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Dtos.Profile;
using ManageYourBudget.Dtos.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageYourBudget.Api.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProfile()
        {
            var result = await _profileService.GetProfile(UserId);
            return Ok(result);
        }

        [HttpPut("password")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var result = await _profileService.ChangePassword(changePasswordDto, UserId);

            if (!result.Succedeed)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPut("password/new")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> AddPassword([FromBody] AddPasswordDto addPasswordDto)
        {
            var result = await _profileService.AddPassword(addPasswordDto, UserId);

            if (!result.Succedeed)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> ChangeProfileInformation([FromBody] UpdateProfileDto updateProfileDto)
        {
            var result = await _profileService.UpdateProfile(updateProfileDto, UserId);

            if (!result.Succedeed)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpGet("search")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PartialSearchResults<UserDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Search([FromQuery] BaseSearchOptionsDto searchOptions)
        {
            var result = await _profileService.Search(searchOptions, UserId);
            return Ok(result);
        }
    }
}
