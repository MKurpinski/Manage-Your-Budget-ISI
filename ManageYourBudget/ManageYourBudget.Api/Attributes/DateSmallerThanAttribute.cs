using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManageYourBudget.Api.Attributes
{
    public class DateSmallerThanAttribute: ValidationAttribute
    {
        public DateSmallerThanAttribute(DateTime? smallerThan)
        {
            SmallerThan = smallerThan ?? DateTime.UtcNow;
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
