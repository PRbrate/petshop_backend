using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Services.Interfaces;
using PetShop.Domain.Entities;

namespace PetShop.Api.Controllers.V1
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UserController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> AuthenticateUser(string RegitrationNumber, string password)
        {
            try
            {
                var response = await _usersService.Authenticate(RegitrationNumber, password);

                if (!response.Success)
                {
                    return UnprocessableEntity(response.Errors);
                }

                return Ok(new { jwt_token = response.Data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

    }
}
