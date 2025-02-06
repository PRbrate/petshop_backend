using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using PetShop.Application.DTO;
using PetShop.Application.Filters;
using PetShop.Application.Services.Interfaces;

namespace PetShop.Api.Controllers.V1
{
    [Route("api/v1/companies")]
    [ApiController]
    
    public class CompanyController : ControllerBase
    {
        private readonly ICompaniesService _companiesService;

        public CompanyController(ICompaniesService companiesService)
        {
            _companiesService = companiesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companiesService.GetAllCompanies();
            if (!companies.Success)
            {
                return UnprocessableEntity(companies.Errors);
            }

            return Ok(companies.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyId(int id)
        {
            var companies = await _companiesService.GetCompany(id);
            if (!companies.Success)
            {
                return UnprocessableEntity(companies.Errors);
            }

            return Ok(companies.Data);
        }

        [HttpGet("/cnpj/{register}")]
        public async Task<IActionResult> GetCompanyRegiter(string register)
        {
            var companies = await _companiesService.GetCompaniesByRegisterNumber(register);
            if (!companies.Success)
            {
                return UnprocessableEntity(companies.Errors);
            }

            return Ok(companies.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompaniesFilter companiesFilter)
        {

            var companies = await _companiesService.CreateCompany(companiesFilter);
            if (!companies.Success)
            {
                return UnprocessableEntity(companies.Errors);
            }

            return Ok(companies.Success);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, CompaniesUpdateDto companiesDto)
        {
            var companies = await _companiesService.UpdateCompany(id, companiesDto);
            if (!companies.Success)
            {
                return UnprocessableEntity(companies.Errors);
            }
            return Ok(companies.Success);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var companies = await _companiesService.DeleteCompany(id);
            if (!companies)
            {
                return UnprocessableEntity(companies);
            }
            return Ok();
        }
    }
}
