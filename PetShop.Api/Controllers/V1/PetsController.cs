using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetShop.Application.DTO;
using PetShop.Application.Services;
using PetShop.Application.Services.Interfaces;
using PetShop.Core.Audit;
using PetShop.Domain.Entities;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace PetShop.Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employer, Admin")]
    public class PetsController : ControllerBase
    {
        private readonly IPetsService _petsService;
        private readonly IAuditHelper _audit;
        private readonly IAuditService _auditService;

        public PetsController(IPetsService petsService, IAuditHelper audit, IAuditService auditService)
        {
            _petsService = petsService;
            _audit = audit;
            _auditService = auditService;
        }

        [HttpPost("CreatePet")]
        public async Task<IActionResult> CreatePet(PetsDto pet)
        {
            try
            {
                var response = await _petsService.CreatePet(pet);

                if (!response.Success)
                {
                    await RegisterLog("PetShop", $"Create Pet fail", new { response.Errors});
                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("PetShop", $"Create Pet", new { response.Success, response.Data });
                return Ok();
            }
            catch (Exception ex)
            {
                await RegisterLog("PetShop", $"Create Pet fail", new { ex.Message });
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
