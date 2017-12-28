using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StepApp.CommonExtensions.Validation
{
    public static class DataAnnotationsValidator
    {
        public static bool TryValidate(object @object, out ICollection<ValidationResult> results)
        {
            var context = new ValidationContext(@object, serviceProvider: null, items: null);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(
                @object, context, results,
                validateAllProperties: true
            );

        }

        public static bool TryValidateProperty(object @object, string fieldName, object fieldValue, out ICollection<ValidationResult> results)
        {
            var context = new ValidationContext(@object, serviceProvider: null, items: null) {MemberName = fieldName};
            results = new List<ValidationResult>();
            return Validator.TryValidateProperty(
                fieldValue, context, results);

        }
    }
}
