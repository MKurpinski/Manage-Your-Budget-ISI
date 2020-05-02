using System;
using System.Net;
using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ManageYourBudget.Api.Controllers
{
    [Route("api/currency")]
    public class CurrencyController: BaseController
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CurrencyRateDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] string currency, [FromQuery] string toCurrency, [FromQuery] DateTime? at)
        {
            var result = await _currencyService.GetCurrencyRate(currency, toCurrency, at);
            return Ok(result);
        }
    }
}
