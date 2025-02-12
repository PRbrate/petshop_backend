using PetShop.Domain.Entities.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PetShop.Application.DTO
{
    public record CompaniesDto(
        int CompanyId, 
        string CompanyName, string TradeName, string RegistrationNumber,
        string Email, string PhoneNumber, string Address, string City,
        string State, string Country, string PostalCode, string Status);
}
