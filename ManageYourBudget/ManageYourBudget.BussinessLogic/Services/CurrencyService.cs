using System;
using System.Threading.Tasks;
using ManageYourBudget.BussinessLogic.ExternalAbstractions;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.Common.Extensions;
using ManageYourBudget.Dtos;

namespace ManageYourBudget.BussinessLogic.Services
{
    public class CurrencyService: ICurrencyService
    {
        private readonly ICacheService _cacheService;
        private readonly IExchangeClient _exchangeClient;

        public CurrencyService(ICacheService cacheService, IExchangeClient exchangeClient)
        {
            _cacheService = cacheService;
            _exchangeClient = exchangeClient;
        }

        public async Task<CurrencyRateDto> GetCurrencyRate(SupportedCurrencies baseCurrency, SupportedCurrencies toCurrency)
        {
            return await GetCurrencyRate(baseCurrency.GetStringValue(), toCurrency.GetStringValue());
        }

        public async Task<CurrencyRateDto> GetCurrencyRate(string baseCurrency, string toCurrency)
        {

            var key = $"{baseCurrency}-{toCurrency}";
            //var fromCache = await _cacheService.Get<CurrencyRateDto>(key);
            //if (fromCache != null)
            //{
            //   return fromCache;
            //}

            var result = await _exchangeClient.GetExchangeRate(baseCurrency, toCurrency);
            if (result != null)
            {
                //await _cacheService.Set(key, result);
            }
            return result;
        }
    }
}
