using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PetShop.Domain.Entities.Validations.Services
{
    public class PhoneNumberValidatorService
    {
        public static bool VerifyPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber)) return false;

            //Regular Expresion fo rcommon format brasilian (with or without DDD and +55)
            string pattern = @"^(\+55\s?)?(\(?\d{2}\)?\s?)?(9\d{4}[-]?\d{4})$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(phoneNumber);
        }
    }
}
