using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ManageYourBudget.Dtos.ValidationAttributes
{
    public class EnumRestrictionAttribute: ValidationAttribute
    {
        private readonly int[] _excludedValues;
        public EnumRestrictionAttribute(params int[] excludedValues)
        {
            _excludedValues = excludedValues;
            ErrorMessage = "Not valid value";
        }

        public override bool IsValid(object value)
        {
            var intValue = (int)value;
            return _excludedValues.All(x => x != intValue);
        }
    }
}