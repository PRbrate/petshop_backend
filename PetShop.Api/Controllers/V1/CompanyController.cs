using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Newtonsoft.Json;
using PetShop.Application.DTO;
using PetShop.Application.Filters;
using PetShop.Application.Services;
using PetShop.Application.Services.Interfaces;
using PetShop.Core.Audit;
using PetShop.Core.Entities;
using PetShop.Domain.Entities;

namespace PetShop.Api.Controllers.V1
{
    [Route("api/v1/companies")]
    [ApiController]

    public class CompanyController : ControllerBase
    {
        private readonly ICompaniesService _companiesService;
        private readonly IAuditHelper _audit;
        private readonly IAuditService _auditService;

        public CompanyController(ICompaniesService companiesService, IAuditHelper audit, IAuditService auditService)
        {
            _companiesService = companiesService;
            _audit = audit;
            _auditService = auditService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCompanies(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var companies = await _companiesService.GetAllCompanies(pageIndex, pageSize);
                if (!companies.Success)
                {
                    await RegisterLog("PetShop", $"Get Companies fail - Admin", new { companies.Errors });
                    return UnprocessableEntity(companies.Errors);
                }

                await RegisterLog("PetShop", $"Get Companies - Admin", new { companies.Success });
                return Ok(companies.Data);
                
            }
            catch (Exception ex)
            {
                await RegisterLog("PetShop", $"Get Companies fail - Admin", new { ex.Message });
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
                    await RegisterLog("PetShop", $"Get Companies fail - Admin", new { companies.Errors });
                    return UnprocessableEntity(companies.Errors);
                }

                await RegisterLog("PetShop", $"Get Companies - Admin", new { companies.Success });
                return Ok(companies.Data);
            }
            catch (Exception ex)
            {
                await RegisterLog("PetShop", $"Get Companies fail - Admin", new { ex.Message });
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("cnpj/{register}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCompanyRegiter(string register)
        {
            try
            {
                var companies = await _companiesService.GetCompaniesByRegisterNumber(register);
                if (!companies.Success)
                {
                    await RegisterLog("PetShop", $"Get Companies fail - Admin", new { companies.Errors });
                    return UnprocessableEntity(companies.Errors);
                }

                await RegisterLog("PetShop", $"Get Companies - Admin", new { companies.Success });
                return Ok(companies.Data);
            }
            catch (Exception ex)
            {
                await RegisterLog("PetShop", $"Get Companies fail - Admin", new { ex.Message });
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
                    await RegisterLog("PetShop", $"Create Companies fail - Admin", new { companies.Errors});
                    return UnprocessableEntity(companies.Errors);
                }

                await RegisterLog("PetShop", $"Create Companies fail - Admin", new {companies.Success, companies.Data});
                return Ok(companies.Success);
            }
            catch (Exception ex)
            {
                await RegisterLog("PetShop", $"Create Companies fail - Admin", new { ex.Message });
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
                    await RegisterLog("PetShop", $"Update Companies fail - Admin", new { companies.Errors });
                    return UnprocessableEntity(companies.Errors);
                }

                await RegisterLog("PetShop", $"Update Companies  - Admin", new {companies.Success, companies.Data });
                return Ok(companies.Success);
            }
            catch (Exception ex)
            {
                await RegisterLog("PetShop", $"Update Companies fail - Admin", new { ex.Message });
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
                    await RegisterLog("PetShop", $"Delete Companies fail - Admin");
                    return UnprocessableEntity(companies);
                }

                await RegisterLog("PetShop", $"Delete Companies - Admin", new { companies });
                return Ok();
            }
            catch (Exception ex) 
            {
                await RegisterLog("PetShop", $"Delete Companies fail - Admin", new { ex.Message });
                return BadRequest(ex.Message);
            }
        }

        protected AuditModel LogAudit(string module, string description, string model)
        {
            return _audit.RegisterLog(HttpContext, module, description, model);
        }

        private async Task RegisterLog(string module, string description, object objectModel = null)
        {
            try
            {
                var modelJson = string.Empty;

                //convert object in json
                if (objectModel != null) modelJson = JsonConvert.SerializeObject(objectModel);

                var log = LogAudit(module, description, modelJson);
                await _auditService.RegisterLog(log);
            }
            catch
            {
                throw;
            }
        }
    }
}
