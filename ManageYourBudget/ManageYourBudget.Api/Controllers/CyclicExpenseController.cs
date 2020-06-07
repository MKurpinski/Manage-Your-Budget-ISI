using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.Factories;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.Dtos.Expense;
using Microsoft.AspNetCore.Mvc;

namespace ManageYourBudget.Api.Controllers
{
    [Route("api/cyclicExpense")]
    public class CyclicExpenseController: BaseController
    {
        private readonly ICyclicExpenseService _expenseService;

        public CyclicExpenseController(ICyclicExpenseService cyclicExpenseService)
        {
            _expenseService = cyclicExpenseService;
        }


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateNew([FromBody] AddCyclicExpenseDto addExpenseDto)
        {
            var result = await _expenseService.CreateExpense(addExpenseDto, UserId);
            if (!result.Succedeed)
            {
                return BadRequest();
            }

            return Ok(result.Value);
        }

        [HttpPut("{expenseId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] AddCyclicExpenseDto addExpenseDto, int expenseId)
        {
            var result = await _expenseService.UpdateExpense(addExpenseDto, expenseId, UserId);
            if (!result.Succedeed)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{expenseId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int expenseId)
        {
            var result = await _expenseService.DeleteExpense(expenseId, UserId);
            if (!result.Succedeed)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpGet("{walletId}")]
        [ProducesResponseType(typeof(List<CyclicExpenseDto>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string walletId)
        {
            var result = await _expenseService.GetAllCyclicExpenses(walletId, UserId);
            return Ok(result);
        }
    }
}
