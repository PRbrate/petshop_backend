using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Entities.Validations.Services
{
    public static class CnpjValidatorService
    {
        public static bool ValidarCnpj(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj)) return false;

            // Remove non-numeric characters
            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

            if (cnpj.Length != 14) return false;

            string[] invalid = {
            "00000000000000", "11111111111111", "22222222222222",
            "33333333333333", "44444444444444", "55555555555555",
            "66666666666666", "77777777777777", "88888888888888",
            "99999999999999"
        };
            if (invalid.Contains(cnpj)) return false;

            int[] multiplicator1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicator2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj[..12];
            int sum = tempCnpj.Select((t, i) => (t - '0') * multiplicator1[i]).Sum();
            int rest = sum % 11;
            int firstDigit = rest < 2 ? 0 : 11 - rest;

            tempCnpj += firstDigit;
            sum = tempCnpj.Select((t, i) => (t - '0') * multiplicator2[i]).Sum();
            rest = sum % 11;
            int secondDigit = rest < 2 ? 0 : 11 - rest;

            return cnpj.EndsWith($"{firstDigit}{secondDigit}");
        }
    }

}
