using System.ComponentModel.DataAnnotations;

namespace TestProject.Validators
{
    public class CallTimeEndValidation : ValidationAttribute
    {
        private readonly string _startTimePropertyName;

        public CallTimeEndValidation(string startTimePropertyName)
        {
            _startTimePropertyName = startTimePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var endTime = (TimeSpan?)value;
            var startTime = (TimeSpan?)validationContext.ObjectType.GetProperty(_startTimePropertyName)
                .GetValue(validationContext.ObjectInstance);

            if (!startTime.HasValue && !endTime.HasValue)
            {
                return ValidationResult.Success; // both null
            }

            if (startTime.HasValue && !endTime.HasValue)
            {
                return new ValidationResult("Call end time is required if start time is provided");
            }

            if (!startTime.HasValue && endTime.HasValue)
            {
                return new ValidationResult("Call start time is required if end time is provided");
            }

            if (startTime.HasValue && endTime.HasValue)
            {
                if (endTime.Value <= startTime.Value)
                {
                    return new ValidationResult("Call end time must be after start time");
                }
            }

            return ValidationResult.Success;
        }
    }
}
