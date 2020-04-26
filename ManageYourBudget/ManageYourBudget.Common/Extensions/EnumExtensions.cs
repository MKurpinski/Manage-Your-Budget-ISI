using System;

namespace ManageYourBudget.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetStringValue<TEnum>(this TEnum value)
        {
            return Enum.GetName(typeof(TEnum), value);
        }
    }
}
