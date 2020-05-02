using System;

namespace ManageYourBudget.Dtos
{
    [Serializable]
    public class CurrencyRateDto
    {
        public string Base { get; set; }
        public string To { get; set; }
        public decimal Rate { get; set; }
    }
}
