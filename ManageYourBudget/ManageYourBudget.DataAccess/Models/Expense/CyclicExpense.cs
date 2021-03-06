﻿using System;
using ManageYourBudget.Common.Enums;

namespace ManageYourBudget.DataAccess.Models.Expense
{
    public class CyclicExpense: BaseExpense
    {
        public CyclicExpensePeriodType PeriodType { get; set; }
        public DateTime StartingFrom { get; set; }
        public DateTime LastApplied { get; set; }
    }
}
