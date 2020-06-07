using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManageYourBudget.Common.Extensions;
using ManageYourBudget.DataAccess.Interfaces;
using ManageYourBudget.Dtos.Expense;
using ManageYourBudget.Dtos.Search;

namespace ManageYourBudget.BussinessLogic.Services
{
    public class ExpenseSearchService: BaseService, IExpenseSearchService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IWalletRepository _walletRepository;

        public ExpenseSearchService(IExpenseRepository expenseRepository, IWalletRepository walletRepository, IMapper mapper) : base(mapper)
        {
            _expenseRepository = expenseRepository;
            _walletRepository = walletRepository;
        }

        public async Task<PagedSearchResults<ExpenseDto>> Search(ExpenseSearchOptionsDto searchOptions, string userId)
        {
            var userWalet = await _walletRepository.Get(searchOptions.WalletId.ToDeobfuscated(), userId);
            if (userWalet == null)
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
