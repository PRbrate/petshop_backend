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
        Task<InternalResponse<CompaniesDto>> CreateCompany(CompaniesFilter companiesFilter);
        Task<InternalResponse<CompaniesDto>> UpdateCompany(int id, CompaniesUpdateDto companiesDto);
        Task<bool> DeleteCompany(int id);
        Task<InternalResponse<CompaniesDto>> GetCompany(int id);
        Task<InternalResponse<List<CompaniesDto>>> GetAllCompanies();
        Task<InternalResponse<CompaniesDto>> GetCompaniesByRegisterNumber(string cnpj);
    }
}
