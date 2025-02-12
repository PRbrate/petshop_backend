using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShop.Application.DTO;
using PetShop.Application.Services.Interfaces;
using PetShop.Core.Entities;
using PetShop.Domain.Entities;

namespace PetShop.Api.Controllers.V1
{
    [Route("api/v1/users")]
    [ApiController]
    [Authorize]

    public class UserController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UserController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(UserDto users, string code)
        {
            try
            {
                var response = await _usersService.CreateUser(users, code);

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
        [Authorize(Roles = "Employer, Admin")]
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
        [Authorize(Roles = "Employer, Admin")]
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

        [HttpGet("GetByRegistrationNumber/{registrationNumber}")]
        [Authorize(Roles = "Employer, Admin")]
        public async Task<IActionResult> GetByRegistrationNumber(string registrationNumber)
        {
            {
                try
                {
                    var response = await _usersService.GetByRegistrationNumber(registrationNumber);

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

        [HttpGet("GetByEmail/{email}")]
        [Authorize(Roles = "Employer, Admin")]
        public async Task<IActionResult> GetById(string email)
        {
            {
                try
                {
                    var response = await _usersService.GetByEmail(email);

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

        [HttpGet("GetByPhoneNumber/{phonenumber}")]
        [Authorize(Roles = "Employer, Admin")]
        public async Task<IActionResult> GetByPhoneNumber(string phonenumber)
        {
            {
                try
                {
                    var response = await _usersService.GetByPhoneNumber(phonenumber);

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
        [Authorize(Roles = "Employer, Admin")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var users = await _usersService.DeleteUser(userId);
            if (!users)
            {
                return UnprocessableEntity(users);
            }
            return Ok();
        }

        [HttpPut("{code}/{userId}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(string code, int userId, UserDto userDto)
        {

            try
            {
                var companies = await _usersService.UpdateUser(userId, userDto, code);
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
