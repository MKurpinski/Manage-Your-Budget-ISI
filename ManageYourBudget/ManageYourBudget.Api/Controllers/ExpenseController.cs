using System.Net;
using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Dtos.Expense;
using Microsoft.AspNetCore.Mvc;

namespace ManageYourBudget.Api.Controllers
{
    [Route("api/expense")]
    public class ExpenseController: BaseController
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateNew([FromBody] ModifyExpenseDto modifyExpenseDto)
        {
            var result = await _expenseService.CreateExpense(modifyExpenseDto, UserId);
            if (!result.Succedeed)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPut("{expenseId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] ModifyExpenseDto modifyExpenseDto, int expenseId)
        {
            var result = await _expenseService.UpdateExpense(modifyExpenseDto, expenseId, UserId);
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
    }
}
