using System;
using System.ComponentModel.DataAnnotations;

namespace ManageYourBudget.Dtos.ValidationAttributes
{
    public class DateSmallerThanNowAttribute: ValidationAttribute
    {
        public DateSmallerThanNowAttribute()
        {
            SmallerThan = DateTime.UtcNow;
            ErrorMessage = $"Date must be earlier than {SmallerThan}";
        }

        public DateTime SmallerThan { get; }

        public override bool IsValid(object value)
        {
            if(!(value is DateTime date))
            {
                return false;
            }
            var difference = SmallerThan - date;
            return difference.Days >= 0;
        }
    }
}
