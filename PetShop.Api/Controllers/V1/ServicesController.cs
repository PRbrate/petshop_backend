using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetShop.Application.DTO;
using PetShop.Application.Services;
using PetShop.Application.Services.Interfaces;
using PetShop.Core.Audit;
using PetShop.Domain.Entities.Enums;

namespace PetShop.Api.Controllers.V1
{
    [Route("api/v1/services")]
    [Authorize(Roles = "Employer, Admin")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesService _servicesService;
        private readonly IAuditHelper _audit;
        private readonly IAuditService _auditService;

        public ServicesController(IServicesService serviceService, IAuditHelper audit, IAuditService auditService)
        {
            _servicesService = serviceService;
            _audit = audit;
            _auditService = auditService;
        }

        [HttpPost("CreateService")]
        public async Task<IActionResult> CreateService(ServiceDto service)
        {
            try
            {
                var response = await _servicesService.CreateService(service);

                if (!response.Success)
                {
                    await RegisterLog("PetShop", $"Create Service fail", new { response.Errors });
                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("PetShop", $"Create Service", new { response.Success, response.Data });
                return Ok();
            }
            catch (Exception ex)
            {
                await RegisterLog("PetShop", $"Create Service fail", new { ex.Message });
                return BadRequest(ex);
            }
        }

        [HttpGet("GetAllServices/")]
        [Authorize]
        public async Task<IActionResult> GetAllServices(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var response = await _servicesService.GetAll(pageIndex, pageSize);

                if (!response.Success)
                {
                    await RegisterLog("PetShop", $"Get Service fail", new { response.Errors });
                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("PetShop", $"Get Services", new { response.Success});
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                await RegisterLog("PetShop", $"Get Service fail", new { ex.Message });
                return BadRequest(ex);
            }
        }

        [HttpGet("GetServicesById/{id}")]
        public async Task<IActionResult> GetPetsById(int id)
        {
            try
            {
                var response = await _servicesService.GetById(id);

                if (!response.Success)
                {
                    await RegisterLog("PetShop", $"Get Service by id fail", new { response.Errors });
                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("PetShop", $"Get Service by id", new { response.Success});
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                await RegisterLog("PetShop", $"Get Service by id fail", new { ex.Message });
                return BadRequest(ex);
            }
        }

        [HttpGet("GetServicesByName/{name}")]
        public async Task<IActionResult> GetPetsByName(string name)
        {
            try
            {
                var response = await _servicesService.GetByName(name);

                if (!response.Success)
                {
                    await RegisterLog("PetShop", $"Get Service by name fail", new { response.Errors });
                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("PetShop", $"Get Service by name", new { response.Success });
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                await RegisterLog("PetShop", $"Get Service by name fail", new { ex.Message });
                return BadRequest(ex);
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
