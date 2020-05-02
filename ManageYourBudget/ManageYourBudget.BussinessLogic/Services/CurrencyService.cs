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

        public async Task<CurrencyRateDto> GetCurrencyRate(SupportedCurrencies baseCurrency, SupportedCurrencies toCurrency, DateTime? at)
        {
            return await GetCurrencyRate(baseCurrency.GetStringValue(), toCurrency.GetStringValue(), at);
        }

        public async Task<CurrencyRateDto> GetCurrencyRate(string baseCurrency, string toCurrency, DateTime? at)
        {
            if (!at.HasValue)
            {
                at = DateTime.UtcNow;
            }

            var key = $"{baseCurrency}-{toCurrency}-{at.Value.ToShortDateString()}";
            var fromCache = await _cacheService.Get<CurrencyRateDto>(key);
            if (fromCache != null)
            {
                return fromCache;
            }

            var result = await _exchangeClient.GetExchangeRate(baseCurrency, toCurrency, at.Value);
            if (result != null)
            {
                await _cacheService.Set(key, result);
            }
            return result;
        }
    }
}
