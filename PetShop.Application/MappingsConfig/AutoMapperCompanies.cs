using PetShop.Application.DTO;
using PetShop.Core.Entities;
using PetShop.Domain.Entities;

namespace PetShop.Application.MappingsConfig
{
    public static class AutoMapperCompanies
    {
        public static Companies CnpjToCompanies(this CnpjResponse cnpjResponse) => new Companies
        {
            CompanyName = cnpjResponse.razao_social,
            TradeName = cnpjResponse.nome_fantasia,
            RegistrationNumber = cnpjResponse.cnpj,
            Address = cnpjResponse.logradouro,
            City = cnpjResponse.municipio,
            State = cnpjResponse.uf,
            PostalCode = cnpjResponse.cep.ToString(),
            Country = "Brasil"
        };

        public static Companies ToCompanies(this CompaniesDto companiesDto) => new Companies
        {
            CompanyId = companiesDto.CompanyId,
            CompanyName = companiesDto.CompanyName,
            TradeName = companiesDto.TradeName,
            RegistrationNumber = new string(companiesDto.RegistrationNumber.Where(char.IsDigit).ToArray()),
            Email = companiesDto.Email,
            PhoneNumber = new string(companiesDto.PhoneNumber.Where(char.IsDigit).ToArray()),
            Address = companiesDto.Address,
            City = companiesDto.City,
            State = companiesDto.State,
            PostalCode = companiesDto.PostalCode,
            Country = "Brazil"
        };

        public static void ToCompanies(this Companies existingCompany, CompaniesUpdateDto companiesDto)
        {
            if (!string.IsNullOrWhiteSpace(companiesDto.CompanyName) && companiesDto.CompanyName != "string")
                existingCompany.CompanyName = companiesDto.CompanyName;

            if (!string.IsNullOrWhiteSpace(companiesDto.TradeName) && companiesDto.TradeName != "string")
                existingCompany.TradeName = companiesDto.TradeName;

            if (!string.IsNullOrWhiteSpace(companiesDto.Email) && companiesDto.Email != "string")
                existingCompany.Email = companiesDto.Email;

            if (!string.IsNullOrWhiteSpace(companiesDto.PhoneNumber) && companiesDto.PhoneNumber != "string")
                existingCompany.PhoneNumber = new string(companiesDto.PhoneNumber.Where(char.IsDigit).ToArray());

            if (!string.IsNullOrWhiteSpace(companiesDto.Address) && companiesDto.Address != "string")
                existingCompany.Address = companiesDto.Address;

            if (!string.IsNullOrWhiteSpace(companiesDto.City) && companiesDto.City != "string")
                existingCompany.City = companiesDto.City;

            if (!string.IsNullOrWhiteSpace(companiesDto.State) && companiesDto.State != "string")
                existingCompany.State = companiesDto.State;

            if (!string.IsNullOrWhiteSpace(companiesDto.PostalCode) && companiesDto.PostalCode != "string")
                existingCompany.PostalCode = companiesDto.PostalCode;
        }

        public static CompaniesDto ToCompaniesDto(this Companies companies) => new
        (
            companies.CompanyId,
            companies.CompanyName,
            companies.TradeName,
            companies.RegistrationNumber,
            companies.Email,
            companies.PhoneNumber,
            companies.Address,
            companies.City,
            companies.State,
            companies.Country,
            companies.PostalCode,
            companies.Status.ToString()
        );


    }
}
