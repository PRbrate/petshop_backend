using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.Services.OtherServices
{
    public class CodeGenerateService
    {
        public static string CodeGenerate(int size = 6)
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var bytes = RandomNumberGenerator.GetBytes(size);
            var result = new StringBuilder(size);

            foreach (var c in bytes)
            {
                result.Append(caracteres[c % caracteres.Length]);
            }
            return result.ToString();
        }
    }
}
