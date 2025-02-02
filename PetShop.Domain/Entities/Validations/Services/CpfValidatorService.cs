using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Entities.Validations.Services
{
    public static class CpfValidatorService
    {
        public static bool ValidateCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            // Remove non-numeric characters
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            // Checks if the CPF has exactly 11 digits
            if (cpf.Length != 11)
                return false;

            // Checks if all digits are the same(ex. 111,111,111 - 11)
            if (cpf.Distinct().Count() == 1)
                return false;

            //Calculates the first check digit
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (cpf[i] - '0') * (10 - i);

            int firstDigitalChecker = sum % 11;
            firstDigitalChecker = firstDigitalChecker < 2 ? 0 : 11 - firstDigitalChecker;

            // Verifica o primeiro dígito
            if (cpf[9] - '0' != firstDigitalChecker)
                return false;

            // Calcula o segundo dígito verificador
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += (cpf[i] - '0') * (11 - i);

            int secondDigitChecker = sum % 11;
            secondDigitChecker = secondDigitChecker < 2 ? 0 : 11 - secondDigitChecker;

            // Verifica o segundo dígito
            return cpf[10] - '0' == secondDigitChecker;
        }
    }
}
