using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Infrastructure.Validation
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
                return false;

            string phoneNumber = value.ToString()!;
            return Regex.IsMatch(phoneNumber, @"^\d{10}$");
        }
    }
}
