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

        public static Companies ToCompanies(this CompaniesUpdateDto companiesDto) => new Companies
        {
            CompanyName = companiesDto.CompanyName,
            TradeName = companiesDto.TradeName,
            Email = companiesDto.Email,
            PhoneNumber = companiesDto.PhoneNumber,
            Address = companiesDto.Address,
            City = companiesDto.City,
            State = companiesDto.State,
            PostalCode = companiesDto.PostalCode,
            Country = "Brazil"
        };

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
