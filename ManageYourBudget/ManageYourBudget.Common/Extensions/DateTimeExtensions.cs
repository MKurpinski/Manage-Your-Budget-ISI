using System;
using System.Collections.Generic;
using System.Text;

namespace ManageYourBudget.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime EndOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }
        public static DateTime StartOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }
    }
}
