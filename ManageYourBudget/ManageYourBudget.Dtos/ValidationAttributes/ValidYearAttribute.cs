using System;
using System.ComponentModel.DataAnnotations;

namespace ManageYourBudget.Dtos.ValidationAttributes
{
    public class ValidYearAttribute: ValidationAttribute
    {
        public ValidYearAttribute()
        {
            SmallerThan = DateTime.UtcNow.Year;
            BiggerThan = DateTime.MinValue.Year;
            ErrorMessage = $"Value must be between {BiggerThan} and {SmallerThan}";
        }

        public int SmallerThan { get; }
        public int BiggerThan { get; }

        public override bool IsValid(object value)
        {
            if(!(value is int year))
            {
                return false;
            }

            return year >= BiggerThan && year <= SmallerThan;
        }
    }
}
