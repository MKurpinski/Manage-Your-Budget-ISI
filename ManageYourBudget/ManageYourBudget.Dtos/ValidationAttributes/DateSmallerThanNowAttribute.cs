using System;
using System.ComponentModel.DataAnnotations;

namespace ManageYourBudget.Dtos.ValidationAttributes
{
    public class DateSmallerThanNowAttribute: ValidationAttribute
    {
        public DateSmallerThanNowAttribute()
        {
            ErrorMessage = $"Date must be earlier than {DateTime.UtcNow}";
        }

        public override bool IsValid(object value)
        {
            if(!(value is DateTime date))
            {
                return false;
            }
            var difference = DateTime.UtcNow - date;
            return difference.Days >= 0;
        }
    }
}
