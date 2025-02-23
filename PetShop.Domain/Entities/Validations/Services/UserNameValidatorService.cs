using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PetShop.Domain.Entities.Validations.Services
{
    public static class UserNameValidatorService
    {
        public static bool ValidaUserName(string username)
        {
            string pattern = @"^(?!\d+$)(?!.*__)[a-zA-Z0-9_]{3,30}$";
            
            return Regex.IsMatch(username, pattern) && !username.StartsWith("_") && !username.EndsWith("_");
        }

    }
}
