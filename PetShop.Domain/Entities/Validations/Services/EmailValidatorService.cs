using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PetShop.Domain.Entities.Validations.Services
{
    public class EmailValidatorService
    {
        public static bool VerifyEmail(string email)
        {
            // Regular expression to check password requirements
            string pattern = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
