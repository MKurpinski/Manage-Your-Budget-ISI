﻿using System;
using System.Threading.Tasks;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.Dtos;

namespace ManageYourBudget.BussinessLogic.Interfaces
{
    public interface ICurrencyService: IService
    {
        Task<CurrencyRateDto> GetCurrencyRate(string baseCurrency, string toCurrency, DateTime? at);

        Task<CurrencyRateDto> GetCurrencyRate(SupportedCurrencies baseCurrency, SupportedCurrencies toCurrency,
            DateTime? at);
    }
}