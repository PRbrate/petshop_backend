using PetShop.Application.DTO;
using PetShop.Application.Filters;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.Services.Interfaces
{
    public interface ICompaniesService
    {
        Task<Response<CompaniesDto>> CreateCompany(CompaniesDto companiesDto);
        Task<Response<CompaniesDto>> UpdateCompany(int id, CompaniesUpdateDto companiesDto);
        Task<bool> DeleteCompany(int id);
        Task<Response<CompaniesDto>> GetCompany(int id);
        Task<Response<List<CompaniesDto>>> GetAllCompanies();
        Task<Response<CompaniesDto>> GetCompaniesByRegisterNumber(string cnpj);
    }
}
