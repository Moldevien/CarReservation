using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Infrastructure.Validation
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string? phoneNumber = value?.ToString();

            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }

            return Regex.IsMatch(phoneNumber, @"^\d{10}$");
        }
    }
}
