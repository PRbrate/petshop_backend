using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetShop.Application.DTO;
using PetShop.Application.Services;
using PetShop.Application.Services.Interfaces;
using PetShop.Core.Audit;
using PetShop.Domain.Entities;
using PetShop.Domain.Entities.Enums;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace PetShop.Api.Controllers.V1
{
    [Route("api/v1/pets")]
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

        [HttpGet("GetAllPets/")]
        [Authorize(Roles = "Employer, Admin")]
        public async Task<IActionResult> GetALlPets(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var response = await _petsService.GetPets(pageIndex, pageSize);

                if (!response.Success)
                {
                    await RegisterLog("PetShop", $"Get Pet fail", new { response.Errors });
                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("PetShop", $"Get Pets", new { response.Success, response.Data });
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                await RegisterLog("PetShop", $"Get Pet fail", new { ex.Message });
                return BadRequest(ex);
            }
        }

        [HttpGet("GetPetsByUser/{id}")]
        [Authorize(Roles = "Employer, Admin")]
        public async Task<IActionResult> GetALlPetsByUser(int id)
        {
            try
            { 
                var response = await _petsService.GetPetByUser(id);

                if (!response.Success)
                {
                    await RegisterLog("PetShop", $"Get Pet by user fail", new { response.Errors });
                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("PetShop", $"Get Pets by id", new { response.Success, response.Data });
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                await RegisterLog("PetShop", $"Get Pet by id fail", new { ex.Message });
                return BadRequest(ex);
            }
        }

        [HttpGet("GetPetsById/{id}")]
        [Authorize(Roles = "Employer, Admin")]
        public async Task<IActionResult> GetPetsById(int id)
        {
            try
            {
                var response = await _petsService.GetPetById(id);

                if (!response.Success)
                {
                    await RegisterLog("PetShop", $"Get Pet by id fail", new { response.Errors });
                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("PetShop", $"Get Pets by id", new { response.Success, response.Data });
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                await RegisterLog("PetShop", $"Get Pet by id fail", new { ex.Message });
                return BadRequest(ex);
            }
        }

        [HttpGet("GetPetsSpecie/")]
        [Authorize(Roles = "Employer, Admin")]
        public async Task<IActionResult> GetPetsBySpecie(Species species, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var response = await _petsService.GetPetsBySpecie(species, pageIndex, pageSize);

                if (!response.Success)
                {
                    await RegisterLog("PetShop", $"Get Pet by specie fail", new { response.Errors });
                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("PetShop", $"Get Pets by specie", new { response.Success, response.Data });
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                await RegisterLog("PetShop", $"Get Pet by specie fail", new { ex.Message });
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPetsGender/")]
        [Authorize(Roles = "Employer, Admin")]
        public async Task<IActionResult> GetPetsByGender(Gender gender, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var response = await _petsService.GetPetsByGender(gender, pageIndex, pageSize);

                if (!response.Success)
                {
                    await RegisterLog("PetShop", $"Get Pet by specie fail", new { response.Errors });
                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("PetShop", $"Get Pets by specie", new { response.Success, response.Data });
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                await RegisterLog("PetShop", $"Get Pet by specie fail", new { ex.Message });
                return BadRequest(ex);
            }
        }

        [HttpGet("GetPetsNeedAttention/")]
        [Authorize(Roles = "Employer, Admin")]
        public async Task<IActionResult> GetPetsNeedAttention(bool attention, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var response = await _petsService.GetNeedAttention(attention, pageIndex, pageSize);

                if (!response.Success)
                {
                    await RegisterLog("PetShop", $"Get Pet by Attention fail", new {code = 422, response.Errors });
                    return UnprocessableEntity(response.Errors);
                }

                await RegisterLog("PetShop", $"Get Pets by Attenttion", new { code = 200, response.Success });
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                await RegisterLog("PetShop", $"Get Pet by specie fail", new {code = 400,  ex.Message });
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
