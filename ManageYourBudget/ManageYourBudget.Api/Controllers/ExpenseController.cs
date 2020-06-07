using System;
using System.Net;
using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.Factories;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.BussinessLogic.Services;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.Common.Extensions;
using ManageYourBudget.Dtos.Expense;
using ManageYourBudget.Dtos.Search;
using Microsoft.AspNetCore.Mvc;

namespace ManageYourBudget.Api.Controllers
{
    [Route("api/expense")]
    public class ExpenseController: BaseController
    {
        private readonly IExpenseService _expenseService;
        private readonly IExpenseSearchService _expenseSearchService;

        public ExpenseController(IExpenseServiceFactory expenseServiceFactory, IExpenseSearchService expenseSearchService)
        {
            _expenseService = expenseServiceFactory.Create(ExpenseType.Normal);
            _expenseSearchService = expenseSearchService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ExpenseDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateNew([FromBody] ModifyExpenseDto modifyExpenseDto)
        {
            var result = await _expenseService.CreateExpense(modifyExpenseDto, UserId);
            if (!result.Succedeed)
            {
                return BadRequest();
            }

            return Ok(result.Value);
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

        [HttpGet]
        [ProducesResponseType(typeof(PagedSearchResults<ExpenseDto>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> Search([FromQuery] ExpenseSearchOptionsDto searchOptions)
        {
            searchOptions.DateFrom = searchOptions.DateFrom.StartOfDay();
            searchOptions.DateTo = searchOptions.DateTo.EndOfDay();
            var result = await _expenseSearchService.Search(searchOptions, UserId);
            return Ok(result);
        }
    }
}
