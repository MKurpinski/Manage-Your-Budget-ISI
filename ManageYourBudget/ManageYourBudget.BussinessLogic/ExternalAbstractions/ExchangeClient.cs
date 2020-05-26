using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ManageYourBudget.Dtos;
using ManageYourBudget.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ManageYourBudget.BussinessLogic.ExternalAbstractions
{
    public interface IExchangeClient: IExternalAbstraction
    {
        Task<CurrencyRateDto> GetExchangeRate(string baseCurrency, string toCurrency);
    }

    public class ExchangeClient : IExchangeClient
    {
        private readonly ExchangeOptions _exchangeOptions;
        private readonly IHttpClientFactory _clientFactory;
        public ExchangeClient(IOptions<ExchangeOptions> fixerOptionsAccessor, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _exchangeOptions = fixerOptionsAccessor.Value;
        }

        public async Task<CurrencyRateDto> GetExchangeRate(string baseCurrency, string toCurrency)
        {
            var at = DateTime.Now;
            var stringAt = at.ToString("yyyy-MM-dd");
            var key = $"{baseCurrency}_{toCurrency}";
            var client = _clientFactory.CreateClient();
            var uri = BuildRequestUrl(key, stringAt);

            var response = await client.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var dynamicData = JObject.Parse(content);
            var rate = Math.Round(dynamicData[key][stringAt].ToObject<decimal>(), 2);
            return new CurrencyRateDto
            {
                Base = baseCurrency,
                To = toCurrency,
                Rate = rate
            };
        }

        private string BuildRequestUrl(string key, string at)
        {
            return $"{_exchangeOptions.BaseUrl}{key}&compact=ultra&date={at}";
        }
    }
}
