using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetShop.Application.DTO;
using PetShop.Application.Services;
using PetShop.Application.Services.Interfaces;
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

        public PetsController(IPetsService petsService)
        {
            _petsService = petsService;
        }
        [HttpPost("CreatePet")]
        public async Task<IActionResult> CreatePet(PetsDto pet)
        {
            try
            {
                var response = await _petsService.CreatePet(pet);

                if (!response.Success)
                {
                    return UnprocessableEntity(response.Errors);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
