using System;
using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.Interfaces;

namespace ManageYourBudget.Jobs
{
    public class CurrencyRateJob: IBudgetJob
    {
        private readonly ICurrencyService _currencyService;
        public CurrencyRateJob(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }
        public async Task Execute()
        {
            await _currencyService.FillCacheWithRates();
        }
    }
}
