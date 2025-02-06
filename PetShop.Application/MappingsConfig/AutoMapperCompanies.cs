using PetShop.Application.DTO;
using PetShop.Core.Entities;
using PetShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            RegistrationNumber = companiesDto.RegistrationNumber,
            Email = companiesDto.Email,
            PhoneNumber = companiesDto.PhoneNumber,
            Address = companiesDto.Address,
            City = companiesDto.City,
            State = companiesDto.State,
            PostalCode = companiesDto.PostalCode,
            Country = "Brazil"
        };

        public static void ToCompanies(this Companies existingCompany, CompaniesUpdateDto companiesDto)
        {
            if (!string.IsNullOrWhiteSpace(companiesDto.CompanyName))
                existingCompany.CompanyName = companiesDto.CompanyName;

            if (!string.IsNullOrWhiteSpace(companiesDto.TradeName))
                existingCompany.TradeName = companiesDto.TradeName;

            if (!string.IsNullOrWhiteSpace(companiesDto.Email))
                existingCompany.Email = companiesDto.Email;

            if (!string.IsNullOrWhiteSpace(companiesDto.PhoneNumber))
                existingCompany.PhoneNumber = new string(companiesDto.PhoneNumber.Where(char.IsDigit).ToArray());

            if (!string.IsNullOrWhiteSpace(companiesDto.Address))
                existingCompany.Address = companiesDto.Address;

            if (!string.IsNullOrWhiteSpace(companiesDto.City))
                existingCompany.City = companiesDto.City;

            if (!string.IsNullOrWhiteSpace(companiesDto.State))
                existingCompany.State = companiesDto.State;

            if (!string.IsNullOrWhiteSpace(companiesDto.PostalCode))
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
