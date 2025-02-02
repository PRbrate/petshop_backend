using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PetShop.Domain.Entities.Validations.Services
{
    public static class PasswordValidatorSerivce
    {
        public static bool VerifyPassword(string password)
        {

            if (password.Length < 8)
                return false;

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return false;
            if (!Regex.IsMatch(password, @"[a-z]"))
                return false;
            if (!Regex.IsMatch(password, @"[\W_]"))
                return false;

            return true;
        }
    }
}
