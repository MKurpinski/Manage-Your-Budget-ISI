using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Common.Extensions;
using ManageYourBudget.DataAccess.Interfaces;
using ManageYourBudget.Dtos.Expense;
using ManageYourBudget.Dtos.Search;

namespace ManageYourBudget.BussinessLogic.Services
{
    public class ExpenseSearchService: BaseService, IExpenseSearchService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IWalletPermissionService _walletPermissionService;

        public ExpenseSearchService(IExpenseRepository expenseRepository, IWalletPermissionService walletPermissionService, IMapper mapper) : base(mapper)
        {
            _expenseRepository = expenseRepository;
            _walletPermissionService = walletPermissionService;
        }

        public async Task<PagedSearchResults<ExpenseDto>> Search(ExpenseSearchOptionsDto searchOptions, string userId)
        {
            var hasUserAccess = await _walletPermissionService.HasUserAccess(searchOptions.WalletId.ToDeobfuscated(), userId);
            if (!hasUserAccess)
            {
                return new PagedSearchResults<ExpenseDto>();
            }

            var resultsFromDb = await _expenseRepository.Search(searchOptions, userId);

            return new PagedSearchResults<ExpenseDto>
            {
                Results = Mapper.Map<List<ExpenseDto>>(resultsFromDb),
                Total = resultsFromDb.FirstOrDefault()?.Total ?? 0
            };
        }
    }
}
