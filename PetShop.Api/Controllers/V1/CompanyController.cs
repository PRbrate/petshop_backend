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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var companies = await _companiesService.GetAllCompanies();
                if (!companies.Success)
                {
                    return UnprocessableEntity(companies.Errors);
                }

                return Ok(companies.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCompanyId(int id)
        {
            try
            {
                var companies = await _companiesService.GetCompany(id);
                if (!companies.Success)
                {
                    return UnprocessableEntity(companies.Errors);
                }

                return Ok(companies.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/cnpj/{register}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCompanyRegiter(string register)
        {
            try
            {
                var companies = await _companiesService.GetCompaniesByRegisterNumber(register);
                if (!companies.Success)
                {
                    return UnprocessableEntity(companies.Errors);
                }

                return Ok(companies.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCompany(CompaniesDto companiesDto)
        {

            try
            {
                var companies = await _companiesService.CreateCompany(companiesDto);
                if (!companies.Success)
                {
                    return UnprocessableEntity(companies.Errors);
                }

                return Ok(companies.Success);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCompany(int id, CompaniesUpdateDto companiesDto)
        {
            try
            {
                var companies = await _companiesService.UpdateCompany(id, companiesDto);
                if (!companies.Success)
                {
                    return UnprocessableEntity(companies.Errors);
                }
                return Ok(companies.Success);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                var companies = await _companiesService.DeleteCompany(id);
                if (!companies)
                {
                    return UnprocessableEntity(companies);
                }
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
