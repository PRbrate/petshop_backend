using Microsoft.AspNetCore.Mvc;
using PetShop.Application.DTO;
using PetShop.Application.Services.Interfaces;
using PetShop.Core.Entities;
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
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(UserDto users)
        {
            try
            {
                var response = await _usersService.CreateUser(users);

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _usersService.GetAll();

                if (!response.Success)
                {
                    return UnprocessableEntity(response.Errors);
                }

                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("GetById/{userId}")]
        public async Task<IActionResult> GetById(int userId)
        {
            {
                try
                {
                    var response = await _usersService.GetById(userId);

                    if (!response.Success)
                    {
                        return UnprocessableEntity(response.Errors);
                    }

                    return Ok(response.Data);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);


                }
            }
        }

        [HttpGet("GetAllByCompanyId/{companyId}")]
        public async Task<IActionResult> GetAllByCompanyId(int companyId)
        {
            {
                try
                {
                    var response = await _usersService.GetAllByCompanyId(companyId);

                    if (!response.Success)
                    {
                        return UnprocessableEntity(response.Errors);
                    }

                    return Ok(response.Data);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);


                }
            }
        }

        [HttpDelete("{userId}")]

        public async Task<IActionResult> DeleteUser(int userId)
        {
            var users = await _usersService.DeleteUser(userId);
            if (!users)
            {
                return UnprocessableEntity(users);
            }
            return Ok();
        }

        [HttpPut("{userId}")]

        public async Task<IActionResult> UpdateUser(int userId, UserDto userDto)
        {

            try
            {
                var companies = await _usersService.UpdateUser(userId, userDto);
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
    }
}
