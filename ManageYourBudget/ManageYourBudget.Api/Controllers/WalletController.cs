using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Dtos.Wallet;
using Microsoft.AspNetCore.Mvc;

namespace ManageYourBudget.Api.Controllers
{
    [Route("api/wallet")]
    //[Authorize]
    public class WalletController: BaseController
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateNew([FromBody] AddWalletDto addWalletDto)
        {
            var result = await _walletService.CreateWallet(addWalletDto, UserId);
            if (!result.Succedeed)
            {
                return BadRequest();
            }
            return Ok(result.Value);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<BaseWalletDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _walletService.GetWallets(UserId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ExtendedWalletDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _walletService.GetWallet(id, UserId);
            if (!result.Succedeed)
            {
                return NotFound();
            }
            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get([FromBody] UpdateWalletDto updateWalletDto, string id)
        {
            var result = await _walletService.UpdateWallet(updateWalletDto, id, UserId);
            if (!result.Succedeed)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Archive(string id)
        {
            var result = await _walletService.ArchiveWallet(id, UserId);
            if (!result.Succedeed)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
