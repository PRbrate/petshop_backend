using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.DTO
{
    public record UserDataDto(int id, string FullName, string UserName, string RegistrationNumber, string Email
        , string Phone, string PostalCode, string State, string City, string Country);

}
