﻿using System;

namespace ManageYourBudget.Common.Enums
{
    [Flags]
    public enum SupportedCurrency
    {
        PLN = 0,
        USD = 1,
        EUR = 2,
        CHF = 3,
        GBP = 4
    }
}
