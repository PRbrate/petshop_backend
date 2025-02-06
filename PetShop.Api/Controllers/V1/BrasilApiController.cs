using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetShop.Application.DTO;
using PetShop.Facade.Interfaces;

namespace PetShop.Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrasilApiController : ControllerBase
    {
        private readonly IBrasilApiHttpService _brasilApiHttpService;

        public BrasilApiController(IBrasilApiHttpService brasilApiHttpService)
        {
            _brasilApiHttpService = brasilApiHttpService;
        }


        [HttpGet("{cnpj}")]
        public async Task<IActionResult> GetCnpj(string cnpj)
        {
            var company = await _brasilApiHttpService.GetCnpj(cnpj);

            return Ok(company.Data);
        }
        [HttpGet("GetCep/{cep}")]
        public async Task<IActionResult> GetCep(string cep)
        {
            var company = await _brasilApiHttpService.GetCep(cep);

            return Ok(company.Data);
        }
    }
}
