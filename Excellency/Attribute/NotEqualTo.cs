using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NotEqualTo : ValidationAttribute
    {
        private const string DefaultErrorMessage = "The value of {0} cannot be the same as the value of the {1}.";

        public string OtherProperty { get; private set; }
        public string OtherPropertyName { get; private set; }

        public NotEqualTo(
            string otherProperty,
            string otherPropertyName)
            : base(DefaultErrorMessage)
        {
            OtherProperty = otherProperty;
            OtherPropertyName = otherPropertyName;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var otherProperty = validationContext.ObjectInstance.GetType().GetProperty(OtherProperty);

                var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);

                if (value.Equals(otherPropertyValue))
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;

        }
    }
}
