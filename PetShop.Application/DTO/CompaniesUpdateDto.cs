using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.DTO
{
    public record CompaniesUpdateDto(
        string CompanyName, string TradeName,string Email, string PhoneNumber, string Address, string City,
        string State, string Country, string PostalCode);

}
