using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetShop.Application.DTO;
using PetShop.Facade.Interfaces;

namespace PetShop.Api.Controllers.V1
{
    [Route("api/v1/BrasilApi")]
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
            try
            {
                var company = await _brasilApiHttpService.GetCnpj(cnpj);

                return Ok(new
                {
                    RegistrationNumber = company.Data.cnpj,
                    CompanyName = company.Data.razao_social,
                    TradeName = company.Data.nome_fantasia,
                    StatusCompany = company.Data.descricao_situacao_cadastral,
                    PostalCode = company.Data.cep,
                    State = company.Data.uf,
                    Adress = company.Data.logradouro,
                    City = company.Data.municipio,
                    PhoneNumber = company.Data.ddd_telefone_1,

                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetCep/{cep}")]
        public async Task<IActionResult> GetCep(string cep)
        {
            try
            {
                var company = await _brasilApiHttpService.GetCep(cep);

                return Ok(new
                {
                    PostalCode = company.Data.cep,
                    State = company.Data.state,
                    City = company.Data.city,
                    Country = company.Data.neighborhood,
                    Street = company.Data.street,

                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
