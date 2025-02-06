using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.DTO
{
    public record UserDataDto(string FullName, string RegistrationNumber, int CompanyId, string Email
        , string Phone, string PostalCode, string State, string City, string Country, string Address);

}
