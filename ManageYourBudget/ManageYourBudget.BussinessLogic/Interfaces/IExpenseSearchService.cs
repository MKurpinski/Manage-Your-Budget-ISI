using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Dtos.Expense;
using ManageYourBudget.Dtos.Search;

namespace ManageYourBudget.BussinessLogic.Services
{
    public interface IExpenseSearchService: IService
    {
        Task<PagedSearchResults<ExpenseDto>> Search(ExpenseSearchOptionsDto searchOptions, string userId);
    }
}
