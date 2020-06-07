using System;
using ManageYourBudget.Common.Enums;

namespace ManageYourBudget.Common
{
    public class DateTimeCalculator
    {
        public static DateTime GetNextApplyingDate(DateTime lastApplyingTime, CyclicExpensePeriodType periodType)
        {
            return periodType == CyclicExpensePeriodType.Month ? lastApplyingTime.AddMonths(1) : lastApplyingTime.AddDays(7);
        }
    }
}
