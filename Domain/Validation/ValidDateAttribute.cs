using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Validation
{
    public class ValidDateAttribute : ValidationAttribute
    {
        /*protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date < DateTime.Today)
                    return new ValidationResult("Дата не може бути в минулому.");
            }
            return ValidationResult.Success;
        }*/
        public override bool IsValid(object? value)
        {
            if (value is DateTime date)
            {
                return date > DateTime.Today;
            }
            return false;
        }

    }
}
