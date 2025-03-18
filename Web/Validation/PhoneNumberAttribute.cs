using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Web.Validation
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
                return false;

            string phoneNumber = value.ToString()!;
            return Regex.IsMatch(phoneNumber, @"^\+380\d{9}$"); // Український формат
        }
    }
}
