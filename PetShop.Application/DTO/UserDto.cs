using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.DTO
{
    public record UserDto(string FullName, string RegistrationNumber, int CompanyId, string Email, string Password
        ,string Phone, string PostalCode, string Address, string City,
        string State, string Country, string Status);
    
}
